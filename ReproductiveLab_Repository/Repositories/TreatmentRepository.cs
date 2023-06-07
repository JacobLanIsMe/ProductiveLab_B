using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForStorage;
using ReproductiveLab_Common.Dtos.ForTreatment;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Common.Models;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Repositories
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly ReproductiveLabContext _db;
        private readonly IMediumRepository _mediumRepository;
        public TreatmentRepository(IMediumRepository mediumRepository, ReproductiveLabContext db)
        {
            _mediumRepository = mediumRepository;
            _db = db;
        }
        public void AddOvumPickup(AddOvumPickupNoteDto ovumPickupNote)
        {
            OvumPickup ovumPickup = new OvumPickup()
            {
                TriggerTime = (DateTime)ovumPickupNote.operationTime.triggerTime,
                StartTime = (DateTime)ovumPickupNote.operationTime.startTime,
                EndTime = (DateTime)ovumPickupNote.operationTime.endTime,
                CocGrade5 = ovumPickupNote.ovumPickupNumber.coc_Grade5,
                CocGrade4 = ovumPickupNote.ovumPickupNumber.coc_Grade4,
                CocGrade3 = ovumPickupNote.ovumPickupNumber.coc_Grade3,
                CocGrade2 = ovumPickupNote.ovumPickupNumber.coc_Grade2,
                CocGrade1 = ovumPickupNote.ovumPickupNumber.coc_Grade1,
                Embryologist = Guid.Parse(ovumPickupNote.embryologist),
                UpdateTime = DateTime.Now
            };
            _mediumRepository.SetMediumInUse<OvumPickup>(ovumPickup, ovumPickupNote.mediumInUse);
            _db.OvumPickups.Add(ovumPickup);
            _db.SaveChanges();
        }
        public Guid GetLatestOvumPickupId()
        {
            return _db.OvumPickups.OrderByDescending(x => x.SqlId).Select(x => x.OvumPickupId).FirstOrDefault();
        }
        
        public BaseTreatmentInfoDto? GetBaseTreatmentInfo(Guid courseOfTreatmentId)
        {
            return _db.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new BaseTreatmentInfoDto
            {
                courseOfTreatmentSqlId = x.SqlId,
                customerSqlId = x.Customer.SqlId,
                customerName = x.Customer.Name,
                birthday = x.Customer.Birthday,
                spouseSqlId = x.Customer.SpouseNavigation.SqlId,
                spouseName = x.Customer.SpouseNavigation.Name,
                doctor = x.DoctorNavigation.Name,
                treatment = new TreatmentDto
                {
                    ovumSituationName = x.OvumSituation.Name,
                    ovumSourceName = x.OvumSource.Name,
                    ovumOperationName = x.OvumOperation.Name,
                    spermSituationName = x.SpermSituation.Name,
                    spermSourceName = x.SpermSource.Name,
                    spermOperationName = x.SpermOperation.Name,
                    spermRetrievalMethodName = x.SpermRetrievalMethod.Name,
                    embryoSituationName = x.EmbryoSituation.Name,
                    embryoOperationName = x.EmbryoOperation.Name
                },
                memo = x.Memo
            }).AsNoTracking().FirstOrDefault();
        }
        public List<TreatmentSummaryModel> GetTreatmentSummary(Guid courseOfTreatmentId)
        {
            return _db.OvumDetails.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new TreatmentSummaryModel
            {
                ovumDetailId = x.OvumDetailId,
                courseOfTreatmentSqlId = x.CourseOfTreatment.SqlId,
                ovumDetailStatus = x.OvumDetailStatus.Name,
                ovumNumber = x.OvumNumber,
                fertilizationTime = x.Fertilization == null ? new DateTime(1753, 1, 1, 0, 0, 0) : x.Fertilization.FertilizationTime,
                fertilizationMethod = x.Fertilization == null ? null : x.Fertilization.FertilizationMethod.Name,
                observationNoteId = x.OvumFreezeId == null ? x.ObservationNotes.Where(y => !y.IsDeleted).OrderByDescending(y => y.SqlId).Select(y => y.ObservationNoteId).FirstOrDefault() : x.ObservationNotes.Where(y => !y.IsDeleted && y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation).Select(y => y.ObservationNoteId).FirstOrDefault(),
                freezeStorageInfo = x.OvumFreeze == null ? null : new BaseStorage
                {
                    tankInfo = new StorageTankDto
                    {
                        tankName = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.StorageTank.TankName,
                        tankTypeId = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.StorageTank.StorageTankTypeId
                    },
                    tankId = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.StorageTankId,
                    canistId = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanistId,
                    canistName = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.CanistName,
                    stripBoxId = x.OvumFreeze.StorageUnit.StorageStripBoxId,
                    stripBoxName = x.OvumFreeze.StorageUnit.StorageStripBox.StripBoxName,
                    topColorName = x.OvumFreeze.TopColor.Name,
                    unitInfo = new StorageUnitDto
                    {
                        storageUnitId = x.OvumFreeze.StorageUnitId,
                        unitName = x.OvumFreeze.StorageUnit.UnitName,
                        isOccupied = x.OvumFreeze.StorageUnit.IsOccupied
                    }
                },
                ovumSource = x.CourseOfTreatment.OvumSource == null ? null : x.CourseOfTreatment.OvumSource.Name,
                ovumFromCourseOfTreatmentSqlId = x.OvumFromCourseOfTreatment.SqlId,
                hasPickup = x.OvumPickupId == null ? false : true,
                isFreshPickup = x.OvumPickupId != null && x.OvumFreezeId == null ? true : false,
                hasFreeze = x.OvumFreezeId != null ? true : false,
                hasTransfer = x.OvumTransferPairRecipientOvumDetails.Any() ? true : false,
                hasThaw = x.OvumThawFreezePairThawOvumDetails.Any() ? true : false,
                isFreezeTransfer = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumThawId == null && x.OvumTransferPairRecipientOvumDetails.Any(y => y.DonorOvumDetail.OvumFreezeId != null) ? true : false,
                isTransferThaw = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumThaw != null ? true : false,
                isFreshTransfer = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumTransferPairRecipientOvumDetails.Any(y => y.DonorOvumDetail.OvumFreezeId == null) ? true : false,
                day_FreshPickup = x.OvumPickupId != null && x.OvumFreezeId == null ? (DateTime.Now.Date - x.CourseOfTreatment.SurgicalTime.Date).Days : 0,
                day_Freeze = x.OvumFreezeId != null ? x.ObservationNotes.Where(y => y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation && !y.IsDeleted).Select(y => y.Day).FirstOrDefault() : 0,
                day_FreezeTransfer = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumThawId == null && x.OvumTransferPairRecipientOvumDetails.Any(y => y.DonorOvumDetail.OvumFreezeId != null) ? x.OvumTransferPairRecipientOvumDetails.Select(y => y.DonorOvumDetail.ObservationNotes.Where(z => z.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation && !z.IsDeleted).Select(z => z.Day).FirstOrDefault()).FirstOrDefault() : 0,
                day_TransferThaw = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumThaw != null ? ((DateTime.Now.Date - x.OvumThaw.ThawTime.Date).Days + x.OvumTransferPairRecipientOvumDetails.Select(y => y.DonorOvumDetail.ObservationNotes.Where(z => z.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation && !z.IsDeleted).Select(z => z.Day).FirstOrDefault()).FirstOrDefault()) : 0,
                day_FreshTransfer = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumTransferPairRecipientOvumDetails.Any(y => y.DonorOvumDetail.OvumFreezeId == null) ? (DateTime.Now.Date - x.OvumTransferPairRecipientOvumDetails.Select(y => y.DonorOvumDetail.CourseOfTreatment.SurgicalTime).FirstOrDefault().Date).Days : 0,
                day_Thaw = x.OvumThawFreezePairThawOvumDetails.Any() && x.OvumThaw != null ? (DateTime.Now.Date - x.OvumThaw.ThawTime.Date).Days + x.OvumThawFreezePairThawOvumDetails.Select(y => y.FreezeOvumDetail.ObservationNotes.Where(z => z.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation && !z.IsDeleted).Select(z => z.Day).FirstOrDefault()).FirstOrDefault() : 0,
            }).OrderBy(x => x.ovumNumber).ToList();
        }
        public List<Common1Dto> GetGermCellSituations()
        {
            return _db.GermCellSituations.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).ToList();
        }
        public List<Common1Dto> GetGermCellSources()
        {
            return _db.GermCellSources.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).ToList();
        }
        public List<Common1Dto> GetGermCellOperations()
        {
            return _db.GermCellOperations.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).ToList();
        }
        public List<Common1Dto> GetSpermRetrievalMethods()
        {
            return _db.SpermRetrievalMethods.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).ToList();
        }
        public List<Common1Dto> GetFertilizationMethods()
        {
            return _db.FertilizationMethods.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).ToList();
        }
        public List<Common1Dto> GetIncubators()
        {
            return _db.Incubators.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).ToList();
        }
        public void AddFertilization(AddFertilizationDto input)
        {
            Fertilization fertilization = new Fertilization
            {
                FertilizationTime = input.fertilizationTime,
                Embryologist = input.embryologist,
                FertilizationMethodId = input.fertilizationMethodId,
                IncubatorId = input.incubatorId,
                OtherIncubator = input.otherIncubator
            };
            _mediumRepository.SetMediumInUse<Fertilization>(fertilization, input.mediumInUseIds);
            _db.Fertilizations.Add(fertilization);
            _db.SaveChanges();
        }
        public Guid GetLatestFertilizationId()
        {
            return _db.Fertilizations.OrderByDescending(x => x.SqlId).Select(x => x.FertilizationId).FirstOrDefault();
        }
        public void AddOvumThaw(AddOvumThawDto input)
        {
            OvumThaw ovumThaw = new OvumThaw
            {
                ThawTime = input.thawTime,
                ThawMediumInUseId = input.thawMediumInUseId,
                Embryologist = input.embryologist,
                RecheckEmbryologist = input.recheckEmbryologist
            };
            _mediumRepository.SetMediumInUse<OvumThaw>(ovumThaw, input.mediumInUseIds);
            _db.OvumThaws.Add(ovumThaw);
            _db.SaveChanges();
        }
        public Guid GetLatestOvumThawId()
        {
            return _db.OvumThaws.OrderByDescending(x => x.SqlId).Select(x => x.OvumThawId).FirstOrDefault();
        }

    }
}
