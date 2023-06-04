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
        
        public TreatmentService(ICourseOfTreatmentRepository courseOfTreatmentRepository, ITreatmentFunction treatmentFunction, ITreatmentRepository treatmentRepository, IObservationNoteService observationNoteService)
        {
            _courseOfTreatmentRepository = courseOfTreatmentRepository;
            _treatmentFunction = treatmentFunction;
            _treatmentRepository = treatmentRepository;
            _observationNoteService = observationNoteService;
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

    }
}
