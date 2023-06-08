using Microsoft.EntityFrameworkCore;
using Reproductive_SharedFunction.Interfaces;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForStorage;
using ReproductiveLab_Common.Dtos.ForTreatment;
using ReproductiveLab_Common.Enums;
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
    public class TreatmentService : ITreatmentService
    {
        private readonly ICourseOfTreatmentRepository _courseOfTreatmentRepository;
        private readonly ITreatmentFunction _treatmentFunction;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IObservationNoteService _observationNoteService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOvumDetailRepository _ovumDetailRepository;
        private readonly IStorageRepository _storageRepository;
        private readonly IOvumFreezeRepository _ovumFreezeRepository;

        public TreatmentService(ICourseOfTreatmentRepository courseOfTreatmentRepository, ITreatmentFunction treatmentFunction, ITreatmentRepository treatmentRepository, IObservationNoteService observationNoteService, ICustomerRepository customerRepository, IOvumDetailRepository ovumDetailRepository, IStorageRepository storageRepository, IOvumFreezeRepository ovumFreezeRepository)
        {
            _courseOfTreatmentRepository = courseOfTreatmentRepository;
            _treatmentFunction = treatmentFunction;
            _treatmentRepository = treatmentRepository;
            _observationNoteService = observationNoteService;
            _customerRepository = customerRepository;
            _ovumDetailRepository = ovumDetailRepository;
            _storageRepository = storageRepository;
            _ovumFreezeRepository = ovumFreezeRepository;
        }
        public List<LabMainPageDto> GetMainPageInfo()
        {
            List<LabMainPageDto> result = _courseOfTreatmentRepository.GetMainPageInfo();
            foreach (var i in result)
            {
                TimeSpan treatmentDay = DateTime.Now.Date - i.surgicalTime.Date;
                i.treatmentDay = $"D{treatmentDay.Days}";
            }
            return result;
        }
        public BaseResponseDto AddOvumPickupNote(AddOvumPickupNoteDto ovumPickupNote)
        {
            BaseResponseDto result = new BaseResponseDto();
            string errorMessage = _treatmentFunction.OvumPickupNoteValidation(ovumPickupNote);
            if (string.IsNullOrEmpty(errorMessage))
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        _treatmentRepository.AddOvumPickup(ovumPickupNote);
                        Guid latestOvumPickupId = _treatmentRepository.GetLatestOvumPickupId();
                        int ovumTotalNumber = ovumPickupNote.ovumPickupNumber.coc_Grade1 + ovumPickupNote.ovumPickupNumber.coc_Grade2 + ovumPickupNote.ovumPickupNumber.coc_Grade3 + ovumPickupNote.ovumPickupNumber.coc_Grade4 + ovumPickupNote.ovumPickupNumber.coc_Grade5;
                        for (int i = 1; i <= ovumTotalNumber; i++)
                        {
                            _ovumDetailRepository.AddOvumDetail(ovumPickupNote.courseOfTreatmentId, ovumPickupNote.courseOfTreatmentId, i, (int)OvumDetailStatusEnum.Incubation, latestOvumPickupId: latestOvumPickupId);
                        }
                        scope.Complete();

                    }
                    result.SetSuccess();
                }
                catch (TransactionAbortedException ex)
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
        public BaseTreatmentInfoDto GetBaseTreatmentInfo(Guid courseOfTreatmentId)
        {
            var result = _treatmentRepository.GetBaseTreatmentInfo(courseOfTreatmentId);
            if (result == null)
            {
                return new BaseTreatmentInfoDto();
            }
            return result;
        }
        public List<TreatmentSummaryDto> GetTreatmentSummary(Guid courseOfTreatmentId)
        {
            var q = _treatmentRepository.GetTreatmentSummary(courseOfTreatmentId);

            List<Guid> observationNoteIds = q.Select(x => x.observationNoteId).ToList();
            var observationNotes = _observationNoteService.GetObservationNoteNameByObservationNoteIds(observationNoteIds);
            List<TreatmentSummaryDto> result = new List<TreatmentSummaryDto>();
            foreach (var i in q)
            {
                TreatmentSummaryDto treatment = new TreatmentSummaryDto
                {
                    ovumDetailId = i.ovumDetailId,
                    courseOfTreatmentSqlId = i.courseOfTreatmentSqlId,
                    ovumDetailStatus = i.ovumDetailStatus,
                    ovumNumber = i.ovumNumber,
                    fertilizationTime = i.fertilizationTime == new DateTime(1753, 1, 1, 0, 0, 0) ? null : i.fertilizationTime,
                    fertilizationMethod = i.fertilizationMethod == null ? null : i.fertilizationMethod,
                    observationNote = observationNotes.FirstOrDefault(x => x.ovumDetailId == i.ovumDetailId),
                    ovumFromCourseOfTreatmentSqlId = i.ovumFromCourseOfTreatmentSqlId,
                    ovumSource = i.ovumSource,
                    freezeStorageInfo = i.freezeStorageInfo
                };
                if (i.hasFreeze)
                {
                    treatment.dateOfEmbryo = i.day_Freeze;
                }
                else if (i.hasPickup)
                {
                    if (i.isFreshPickup)
                    {
                        treatment.dateOfEmbryo = i.day_FreshPickup;
                    }
                    else
                    {
                        treatment.dateOfEmbryo = default;
                    }
                }
                else if (i.hasTransfer)
                {
                    if (i.isFreezeTransfer)
                    {
                        treatment.dateOfEmbryo = i.day_FreezeTransfer;
                    }
                    else if (i.isTransferThaw)
                    {
                        treatment.dateOfEmbryo = i.day_TransferThaw;
                    }
                    else if (i.isFreezeTransfer)
                    {
                        treatment.dateOfEmbryo = i.day_FreshTransfer;
                    }
                    else
                    {
                        treatment.dateOfEmbryo = default;
                    }
                }
                else
                {
                    treatment.dateOfEmbryo = i.day_Thaw;
                }
                result.Add(treatment);
            }
            return result;
        }
        public List<Common1Dto> GetGermCellSituations()
        {
            return _treatmentRepository.GetGermCellSituations();
        }
        public List<Common1Dto> GetGermCellSources()
        {
            return _treatmentRepository.GetGermCellSources();
        }
        public List<Common1Dto> GetGermCellOperations()
        {
            return _treatmentRepository.GetGermCellOperations();
        }
        public List<Common1Dto> GetSpermRetrievalMethods()
        {
            return _treatmentRepository.GetSpermRetrievalMethods();
        }
        public BaseResponseDto AddCourseOfTreatment(AddCourseOfTreatmentDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _courseOfTreatmentRepository.AddCourseOfTreatment(input);
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
        public BaseResponseDto AddOvumFreeze(AddOvumFreezeDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                AddOvumFreezeValidation(input);
                using (TransactionScope scope = new TransactionScope())
                {
                    _ovumFreezeRepository.AddOvumFreeze(input);
                    Guid latestOvumFreezeId = _ovumFreezeRepository.GetLatestOvumFreezedId();
                    var ovumDetails = _ovumDetailRepository.GetOvumDetailByIds(input.ovumDetailId);
                    _ovumDetailRepository.UpdateOvumDetailToFreeze(ovumDetails, latestOvumFreezeId);
                    var storageUnit = _storageRepository.GetStorageUnitById(input.storageUnitId);
                    if (storageUnit != null)
                    {
                        _storageRepository.UpdateStorageUnitToOccupied(storageUnit);
                    }
                    else
                    {
                        throw new Exception("儲位資訊有誤");
                    }
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
        private void AddOvumFreezeValidation(AddOvumFreezeDto input)
        {
            if (input.ovumDetailId.Count <= 0 || input.ovumDetailId.Count > 4)
            {
                throw new Exception("卵子數量請介於 1-4");
            }
            var isFreezed = _ovumDetailRepository.GetFreezeOvumDetailByIds(input.ovumDetailId);
            if (isFreezed != null)
            {
                throw new Exception($"卵子編號: {isFreezed.OvumNumber} 已冷凍入庫");
            }
            var hasFreezeObservationNote = _ovumDetailRepository.GetFreezeObservationOvumDetailByIds(input.ovumDetailId);
            if (hasFreezeObservationNote != null)
            {
                throw new Exception($"卵子編號: {hasFreezeObservationNote.OvumNumber} 尚無冷凍觀察紀錄");
            }
        }
        public BaseResponseDto UpdateOvumFreeze(AddOvumFreezeDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (input.ovumDetailId.Count <= 0)
                    {
                        throw new Exception("請選擇要修改的卵子");
                    }
                    var ovumFreeze = _ovumFreezeRepository.GetOvumFreezeByOvumDetailId(input.ovumDetailId[0]);
                    if (ovumFreeze == null)
                    {
                        throw new Exception("無相關的卵子資訊");
                    }
                    _ovumFreezeRepository.UpdateOvumFreeze(ovumFreeze, input);
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
        public AddOvumFreezeDto GetOvumFreeze(Guid ovumDetailId)
        {
            var ovumFreeze = _ovumFreezeRepository.GetOvumFreezeDtoByOvumDetailId(ovumDetailId);
            if (ovumFreeze == null)
            {
                return new AddOvumFreezeDto();
            }
            return ovumFreeze;
        }
        public BaseCustomerInfoDto GetOvumOwnerInfo(Guid ovumDetailId)
        {
            var result = _customerRepository.GetBaseCustomerInfoByOvumDetailId(ovumDetailId);
            if (result == null)
            {
                return new BaseCustomerInfoDto();
            }
            return result;
        }
        
        public List<Common1Dto> GetFertilizationMethods()
        {
            return _treatmentRepository.GetFertilizationMethods();
        }
        public List<Common1Dto> GetIncubators()
        {
            return _treatmentRepository.GetIncubators();
        }
        public BaseResponseDto AddFertilization(AddFertilizationDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _treatmentRepository.AddFertilization(input);
                    Guid latestFertilizationId = _treatmentRepository.GetLatestFertilizationId();
                    if (latestFertilizationId == Guid.Empty)
                    {
                        throw new Exception("Fertilisation 資料表寫入也誤");
                    }
                    var ovumDetailIs = _ovumDetailRepository.GetOvumDetailByIds(input.ovumDetailIds);
                    _ovumDetailRepository.UpdateOvumDetailToFertilization(ovumDetailIs, latestFertilizationId);
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
        public BaseResponseDto AddOvumThaw(AddOvumThawDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                AddOvumThawValidation(input);
                using (TransactionScope scope = new TransactionScope())
                {
                    #region 增加一筆 OvumThaw 的資訊，並得到此筆 OvumThaw 的 OvumThawId
                    _treatmentRepository.AddOvumThaw(input);
                    Guid latestOvumThawId = _treatmentRepository.GetLatestOvumThawId();
                    #endregion

                    var freezeOvumDetails = _ovumDetailRepository.GetFreezeOvumDetailModelByIds(input.freezeOvumDetailIds);
                    _ovumDetailRepository.UpdateFreezeOvumDetail(freezeOvumDetails, latestOvumThawId);
                    int count = 0;
                    foreach (var i in freezeOvumDetails.ToList())
                    {
                        if (i.observationNoteCount == 0 && i.isTransferred) { }
                        else
                        {
                            _ovumDetailRepository.AddOvumDetail(input.courseOfTreatmentId, i.ovumDetail.OvumFromCourseOfTreatmentId, count + 1, (int)OvumDetailStatusEnum.Incubation, latestOvumThawId: latestOvumThawId, fertilizationId: i.ovumDetail.FertilizationId);

                            Guid thawOvumDetailId = _ovumDetailRepository.GetLatestOvumDetailId();
                            _treatmentRepository.AddOvumThawFreezePair(i.ovumDetail.OvumDetailId, thawOvumDetailId);
                        }
                        count++;
                    }
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
        private void AddOvumThawValidation(AddOvumThawDto input)
        {
            if (input.freezeOvumDetailIds == null || input.freezeOvumDetailIds.Count <= 0)
            {
                throw new Exception("請選擇要解凍的卵子");
            }
            if (input.mediumInUseIds == null || input.mediumInUseIds.Count <= 0)
            {
                throw new Exception("請選擇培養液");
            }
            var hasThawedOvumDetails = _ovumDetailRepository.GetThawOvumDetailByIds(input.freezeOvumDetailIds);
            if (hasThawedOvumDetails.Any())
            {
                string errorMessage = "";
                foreach (var i in hasThawedOvumDetails)
                {
                    string message = $"療程編號: {i.courseOfTreatmentSqlId} ，卵子編號: ";
                    foreach (var j in i.ovumNumbers)
                    {
                        message += $"{j}, ";
                    }
                    message = message.Substring(0, message.Length - 2);
                    message += "已解凍\n";
                    errorMessage += message;
                }
                throw new Exception(errorMessage);
            }
        }
        public BaseResponseDto OvumBankTransfer(OvumBankTransferDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                if (input.transferOvumDetailIds == null || input.transferOvumDetailIds.Count < 1)
                {
                    throw new Exception("請選擇要轉移的卵子");
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    // 將受贈者的OvumFromCourseOfTreatmentId 改成捐贈者的 CourseOfTreatmentId
                    var recipientCourseOfTreatmentId = _courseOfTreatmentRepository.GetCourseOfTreatmentIdBySqlId(input.recipientCourseOfTreatmentSqlId);
                    if (recipientCourseOfTreatmentId == Guid.Empty)
                    {
                        throw new Exception("查無此受贈者之療程編號");
                    }

                    var donorOvumDetails = _ovumDetailRepository.GetOvumDetailByIds(input.transferOvumDetailIds).ToList();
                    if (donorOvumDetails.Count != input.transferOvumDetailIds.Count)
                    {
                        throw new Exception("捐贈卵子資訊有誤");
                    }
                    for (int i = 0; i < donorOvumDetails.Count; i++)
                    {
                        // 加入新的 OvumDetail 資料
                        int ovumDetailStatusId = donorOvumDetails[i].OvumFreezeId == null ? (int)OvumDetailStatusEnum.Incubation : (int)OvumDetailStatusEnum.Freeze;
                        _ovumDetailRepository.AddOvumDetail(recipientCourseOfTreatmentId, donorOvumDetails[i].OvumFromCourseOfTreatmentId, i + 1, ovumDetailStatusId, fertilizationId: donorOvumDetails[i].FertilizationId, ovumFreezeId: donorOvumDetails[i].OvumFreezeId);

                        Guid latestOvumDetailId = _ovumDetailRepository.GetLatestOvumDetailId();
                        // 加入新的 OvumTransferPair 資料
                        _treatmentRepository.AddOvumTransferPair(latestOvumDetailId, donorOvumDetails[i].OvumDetailId);
                    }
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
