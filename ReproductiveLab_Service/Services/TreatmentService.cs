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
        
        public TreatmentService(ICourseOfTreatmentRepository courseOfTreatmentRepository, ITreatmentFunction treatmentFunction, ITreatmentRepository treatmentRepository, IObservationNoteService observationNoteService, ICustomerRepository customerRepository, IOvumDetailRepository ovumDetailRepository)
        {
            _courseOfTreatmentRepository = courseOfTreatmentRepository;
            _treatmentFunction = treatmentFunction;
            _treatmentRepository = treatmentRepository;
            _observationNoteService = observationNoteService;
            _customerRepository = customerRepository;
            _ovumDetailRepository = ovumDetailRepository;
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
                            _treatmentRepository.AddOvumDetail(ovumPickupNote, latestOvumPickupId, i);
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
        public List<BaseCustomerInfoDto> GetAllCustomer()
        {
            return _customerRepository.GetAllCustomer();
        }
        public BaseCustomerInfoDto GetCustomerByCustomerSqlId(int customerSqlId)
        {
            return _customerRepository.GetBaseCustomerInfoBySqlId(customerSqlId);
        }
        public BaseCustomerInfoDto GetCustomerByCourseOfTreatmentId(Guid courseOfTreatmentId)
        {
            return _customerRepository.GetBaseCustomerInfoByCourseOfTreatmentId(courseOfTreatmentId);
        }
        public async Task<BaseResponseDto> AddCourseOfTreatment(AddCourseOfTreatmentDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _treatmentRepository.AddCourseOfTreatment(input);
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
        public async Task<BaseResponseDto> AddOvumFreeze(AddOvumFreezeDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                AddOvumFreezeValidation(input);
                using (TransactionScope scope = new TransactionScope())
                {
                    OvumFreeze ovumFreeze = new OvumFreeze
                    {
                        FreezeTime = input.freezeTime,
                        Embryologist = input.embryologist,
                        StorageUnitId = input.storageUnitId,
                        MediumInUseId = input.mediumInUseId,
                        OtherMediumName = input.otherMediumName,
                        Memo = input.memo,
                        OvumMorphologyA = input.ovumMorphology_A,
                        OvumMorphologyB = input.ovumMorphology_B,
                        OvumMorphologyC = input.ovumMorphology_C,
                        TopColorId = input.topColorId,
                        IsThawed = false
                    };
                    dbContext.OvumFreezes.Add(ovumFreeze);
                    dbContext.SaveChanges();
                    Guid latestOvumFreezeId = dbContext.OvumFreezes.OrderByDescending(x => x.SqlId).Select(x => x.OvumFreezeId).FirstOrDefault();
                    var ovumDetails = dbContext.OvumDetails.Where(x => input.ovumDetailId.Contains(x.OvumDetailId));
                    foreach (var i in ovumDetails)
                    {
                        i.OvumDetailStatusId = (int)OvumDetailStatusEnum.Freeze;
                        i.OvumFreezeId = latestOvumFreezeId;
                    }
                    dbContext.SaveChanges();
                    var storageUnit = dbContext.StorageUnits.FirstOrDefault(x => x.SqlId == input.storageUnitId);
                    if (storageUnit != null)
                    {
                        storageUnit.IsOccupied = true;
                    }
                    else
                    {
                        throw new Exception("儲位資訊有誤");
                    }
                    dbContext.SaveChanges();
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
            var hasFreezeObservationNote = dbContext.OvumDetails.Where(x => input.ovumDetailId.Contains(x.OvumDetailId)).Include(x => x.ObservationNotes).FirstOrDefault(x => !x.ObservationNotes.Any(y => y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation));
            if (hasFreezeObservationNote != null)
            {
                throw new Exception($"卵子編號: {hasFreezeObservationNote.OvumNumber} 尚無冷凍觀察紀錄");
            }
        }
    }
}
