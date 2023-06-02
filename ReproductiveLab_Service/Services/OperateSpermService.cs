using Microsoft.EntityFrameworkCore;
using Reproductive_SharedFunction.Interfaces;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForOperateSperm;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Service.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ReproductiveLab_Service.Services
{
    public class OperateSpermService : IOperateSpermService
    {
        private readonly IOperateSpermRepository _operateSpermRepository;
        private readonly ICourseOfTreatmentRepository _courseOfTreatmentRepository;
        private readonly IOperateSpermFunction _operateSpermFunction;
        public OperateSpermService(IOperateSpermRepository operateSpermRepository, ICourseOfTreatmentRepository courseOfTreatmentRepository, IOperateSpermFunction operateSpermFunction)
        {
            _operateSpermRepository = operateSpermRepository;
            _courseOfTreatmentRepository = courseOfTreatmentRepository;
            _operateSpermFunction = operateSpermFunction;
        }

        public List<SpermFreezeDto> GetSpermFreeze(int customerSqlId)
        {
            return _operateSpermRepository.GetSpermFreeze(customerSqlId);
        }
        public List<SpermScoreDto> GetSpermScores(Guid courseOfTreatmentId)
        {
            var courseOfTreatment = _courseOfTreatmentRepository.GetCourseOfTreatmentById(courseOfTreatmentId);
            if (courseOfTreatment == null)
            {
                return new List<SpermScoreDto>();
            }
            var q = _operateSpermRepository.GetSpermScoresByCourseOfTreatmentId(courseOfTreatmentId);
            List<SpermScoreDto> result = new List<SpermScoreDto>();
            foreach (var i in q)
            {
                SpermScoreDto spermScore = new SpermScoreDto
                {
                    volume = i.volume,
                    concentration = i.concentration,
                    activityA = i.activityA,
                    activityB = i.activityB,
                    activityC = i.activityC,
                    activityD = i.activityD,
                    morphology = i.morphology,
                    abstinence = i.abstinence,
                    spermScoreTimePointId = i.spermScoreTimePointId,
                    spermScoreTimePoint = i.spermScoreTimePoint,
                    recordTime = i.recordTime,
                    embryologist = i.embryologist,
                    embryologistName = i.embryologistName,
                    courseOfTreatmentId = i.courseOfTreatmentId,
                    courseOfTreatmentSqlId = i.courseOfTreatmentSqlId
                };
                spermScore.baseSpermInfo = i.isThawed ? i.baseSpermInfo_Thaw : i.baseSpermInfo_Fresh;
                result.Add(spermScore);
            }
            return result;
        }
        public BaseResponseDto AddSpermScore(SpermScoreDto addSpermScore)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _operateSpermRepository.AddSpermScore(addSpermScore);
                    scope.Complete();
                }

                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }
        public BaseResponseDto UpdateExistingSpermScore(SpermScoreDto addSpermScore)
        {
            BaseResponseDto result = new BaseResponseDto();
            var existingSpermScore = _operateSpermRepository.GetExistingSpermScoreByCourseOfTreatmentId(addSpermScore.courseOfTreatmentId, addSpermScore.spermScoreTimePointId);
            if (existingSpermScore != null)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        _operateSpermRepository.UpdateSpermScore(addSpermScore, existingSpermScore);
                        scope.Complete();
                    }
                    result.SetSuccess();
                }
                catch (Exception ex)
                {
                    result.SetError(ex.Message);
                }
            }
            else
            {
                result.SetError("找無此紀錄");
            }
            return result;
        }
        public List<Common1Dto> GetSpermFreezeOperationMethod()
        {
            return _operateSpermRepository.GetSpermFreezeOperationMethod();
        }
        public BaseResponseDto AddSpermFreeze(AddSpermFreezeDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            string errorMessage = _operateSpermFunction.AddSpermFreezeValidation(input);
            if (string.IsNullOrEmpty(errorMessage))
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        _operateSpermRepository.AddSpermFreezeSituation(input);
                        Guid lastSituationId = _operateSpermRepository.GetLatestSpermFreezeSituationId();
                        if (lastSituationId == Guid.Empty)
                        {
                            throw new Exception("冷凍精子狀況有誤");
                        }
                        _operateSpermRepository.AddSpermFreeze(input, lastSituationId);
                        scope.Complete();
                    }
                    result.SetSuccess();
                }
                catch (Exception ex)
                {
                    result.SetError(ex.Message);
                }
            }
            else
            {
                result.SetError(errorMessage);
            }

            return result;
        }
        public List<Common1Dto> GetSpermThawMethods()
        {
            return _operateSpermRepository.GetSpermThawMethods();
        }
        public BaseResponseDto AddSpermThaw(AddSpermThawDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _operateSpermRepository.AddSpermThaw(input);
                    Guid latestSpermThawId = _operateSpermRepository.GetLatestSpermThawId();
                    if (latestSpermThawId == Guid.Empty)
                    {
                        throw new Exception("spermThaw 資料表寫入錯誤");
                    }
                    _operateSpermRepository.AddSpermThawFreezePair(input.spermFreezeIds, latestSpermThawId);
                    scope.Complete();
                }
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }



    }
}
