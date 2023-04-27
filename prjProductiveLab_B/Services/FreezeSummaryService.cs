﻿using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForFreezeSummary;
using prjProductiveLab_B.Dtos.ForObservationNote;
using prjProductiveLab_B.Dtos.ForStorage;
using prjProductiveLab_B.Enums;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;
using System.Runtime.CompilerServices;

namespace prjProductiveLab_B.Services
{
    public class FreezeSummaryService : IFreezeSummaryService
    {
        private readonly ReproductiveLabContext dbContext;
        private readonly IWebHostEnvironment env;
        public FreezeSummaryService(ReproductiveLabContext dbContext, IWebHostEnvironment env)
        {
            this.dbContext = dbContext;
            this.env = env;
        }
        public async Task<List<GetOvumFreezeSummaryDto>> GetRecipientOvumFreezes(Guid courseOfTreatmentId)
        {
            var customerId = dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => x.CustomerId).FirstOrDefault();
            if (customerId == Guid.Empty)
            {
                return new List<GetOvumFreezeSummaryDto>();
            }
            var result = await dbContext.OvumPickupDetails.Where(x => x.OvumPickup.CourseOfTreatment.CustomerId == customerId && x.Fertilisation == null && x.OvumThaw == null && x.OvumFreeze != null).Select(x => new GetOvumFreezeSummaryDto
            {
                courseOfTreatmentSqlId = x.OvumPickup.CourseOfTreatment.SqlId,
                courseOfTreatmentId = x.OvumPickup.CourseOfTreatmentId,
                ovumNumber = x.OvumNumber,
                ovumPickupTime = x.OvumPickup.StartTime,
                freezeTime = x.OvumFreeze.FreezeTime,
                freezeObservationNoteInfo = dbContext.ObservationNotes.Where(y => y.OvumPickupDetailId == x.OvumPickupDetailId && y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation).Include(y => y.ObservationNotePhotos).Include(y => y.ObservationNoteOvumAbnormalities).OrderByDescending(y => y.ObservationTime).Select(y => new GetObservationNoteNameDto
                {
                    ovumPickupDetailId = y.OvumPickupDetailId,
                    observationTime = y.ObservationTime,
                    memo = y.Memo,
                    day = y.Day,
                    ovumMaturationName = y.OvumMaturation.Name,
                    ovumAbnormalityName = y.ObservationNoteOvumAbnormalities.Select(z => z.ForeignKey.Name).ToList(),
                    observationNotePhotos = y.ObservationNotePhotos.Where(z => z.IsMainPhoto).Select(z => new ObservationNotePhotoDto
                    {
                        observationNotePhotoId = z.ObservationNotePhotoId,
                        photoName = z.PhotoName,
                    }).ToList(),
                }).FirstOrDefault(),
                freezeStorageInfo = new BaseStorage
                {
                    tankInfo = new StorageTankDto
                    {
                        tankName = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.StorageTank.TankName,
                        tankTypeId = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.StorageTank.StorageTankTypeId
                    },
                    canistName = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.CanistName,
                    stripBoxId = x.OvumFreeze.StorageUnit.StorageStripBoxId,
                    unitInfo = new StorageUnitDto
                    {
                        storageUnitId = x.OvumFreeze.StorageUnitId,
                        unitName = x.OvumFreeze.StorageUnit.UnitName
                    }
                }
            }).OrderBy(x => x.freezeTime).ThenBy(x => x.ovumNumber).ToListAsync();

            ConvertPhotoToBase64String(result);
            return result;
        }

        private void ConvertPhotoToBase64String(List<GetOvumFreezeSummaryDto> result)
        {
            foreach (var i in result)
            {
                if (i.freezeObservationNoteInfo != null && i.freezeObservationNoteInfo.observationNotePhotos != null && i.freezeObservationNoteInfo.observationNotePhotos.Count > 0 && !string.IsNullOrEmpty(i.freezeObservationNoteInfo.observationNotePhotos[0].photoName))
                {
                    string path = Path.Combine(env.ContentRootPath, "uploads", "images", i.freezeObservationNoteInfo.observationNotePhotos[0].photoName);
                    if (File.Exists(path))
                    {
                        i.freezeObservationNoteInfo.observationNotePhotos[0].imageBase64String = Convert.ToBase64String(File.ReadAllBytes(path));
                    }
                }
            }
        }
        public async Task<List<GetOvumFreezeSummaryDto>> GetOvumFreezeSummary(Guid courseOfTreatmentId)
        {
            var customerId = dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => x.CustomerId).FirstOrDefault();
            var result = await dbContext.OvumPickupDetails.Where(x => (x.OvumPickup.CourseOfTreatment.CustomerId == customerId || x.OvumThaw.CourseOfTreatment.CustomerId == customerId) && x.OvumFreezeId != null).Select(x => new GetOvumFreezeSummaryDto
            {
                courseOfTreatmentSqlId = x.OvumPickup.CourseOfTreatment.SqlId,
                courseOfTreatmentId = x.OvumPickup.CourseOfTreatmentId,
                ovumFromCourseOfTreatmentSqlId = dbContext.CourseOfTreatments.Where(y => y.CourseOfTreatmentId == x.OvumPickup.CourseOfTreatment.OvumFromCourseOfTreatmentId).Select(y => y.SqlId).FirstOrDefault(),
                ovumFromCourseOfTreatmentId = x.OvumPickup.CourseOfTreatment.OvumFromCourseOfTreatmentId,
                ovumNumber = x.OvumNumber,
                ovumPickupTime = x.OvumPickup.StartTime,
                freezeTime = x.OvumFreeze.FreezeTime,
                freezeObservationNoteInfo = dbContext.ObservationNotes.Where(y => y.OvumPickupDetailId == x.OvumPickupDetailId && y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation).Include(y => y.ObservationNotePhotos).OrderByDescending(y => y.ObservationTime).Select(y => new GetObservationNoteNameDto
                {
                    observationNoteId = y.ObservationNoteId,
                    day = y.Day,
                    memo = y.Memo,
                    fertilisationResultName = y.FertilisationResult.Name,
                    blastomereScore_C_Name = y.BlastomereScoreC.Name,
                    blastomereScore_G_Name = y.BlastomereScoreG.Name,
                    blastomereScore_F_Name = y.BlastomereScoreF.Name,
                    blastocystScore_Expansion_Name = y.BlastocystScoreExpansion.Name,
                    blastocystScore_TE_Name = y.BlastocystScoreTe.Name,
                    blastocystScore_ICE_Name = y.BlastocystScoreIce.Name,
                    observationNotePhotos = y.ObservationNotePhotos.Where(z => z.IsMainPhoto == true && z.IsDeleted == false).Select(z => new ObservationNotePhotoDto
                    {
                        observationNotePhotoId = z.ObservationNotePhotoId,
                        photoName = z.PhotoName,
                        isMainPhoto = true
                    }).ToList(),
                    kidScore = y.Kidscore.ToString(),
                    pgtaNumber = y.Pgtanumber.ToString(),
                    pgtaResult = y.Pgtaresult,
                    pgtmResult = y.Pgtmresult
                }).FirstOrDefault(),
                freezeStorageInfo = new BaseStorage
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
                medium = x.OvumFreeze.MediumInUse.MediumTypeId == (int)MediumTypeEnum.other ? x.OvumFreeze.OtherMediumName : x.OvumFreeze.MediumInUse.Name,

            }).OrderBy(x => x.ovumPickupTime).ThenBy(x => x.ovumNumber).ToListAsync();
            ConvertPhotoToBase64String(result);
            return result;

        }
        public async Task<List<GetSpermFreezeSummaryDto>> GetSpermFreezeSummary(Guid courseOfTreatmentId)
        {
            var q = await dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new
            {
                treatment = x.Treatment,
                course = x,
                husbandID = x.Customer.SpouseNavigation.CustomerId
            }).FirstOrDefaultAsync();
            if (q == null)
            {
                return new List<GetSpermFreezeSummaryDto>();
            }
            Guid customerId = q.treatment.SpermOperationId == (int)GermCellOperationEnum.freeze ? q.course.CustomerId : q.husbandID;
            var result = await dbContext.SpermFreezes.Where(x=>x.CourseOfTreatment.CustomerId == customerId).Select(x=>new GetSpermFreezeSummaryDto
            {
                spermSourceName = x.CourseOfTreatment.Treatment.SpermSource.Name,
                courseOfTreatmentSqlId = x.CourseOfTreatment.SqlId,
                spermSituation = x.CourseOfTreatment.Treatment.SpermSituation.Name,
                surgicalTime = x.CourseOfTreatment.SurgicalTime,
                freezeTime = x.SpermFreezeSituation.FreezeTime,
                vialNumber = x.VialNumber,
                tankName = x.StorageUnit.StorageStripBox.StorageCanist.StorageTank.TankName,
                canistName = x.StorageUnit.StorageStripBox.StorageCanist.CanistName,
                boxId = x.StorageUnit.StorageStripBoxId,
                unitName = x.StorageUnit.UnitName,
                freezeMediumName = x.SpermFreezeSituation.FreezeMediumInUse.MediumTypeId == (int)MediumTypeEnum.other ? x.SpermFreezeSituation.OtherFreezeMediumName : x.SpermFreezeSituation.FreezeMediumInUse.Name,
            }).OrderBy(x=>x.freezeTime).ThenBy(x=>x.vialNumber).ToListAsync();
            return result;
        }

        
    }
}
