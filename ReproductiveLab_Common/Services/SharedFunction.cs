using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using ReproductiveLab_Common.Dtos.ForStorage;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReproductiveLabDB.Models;
using ReproductiveLab_Common.Enums;
using Microsoft.AspNetCore.Hosting;

namespace ReproductiveLab_Common.Services
{
    public  class SharedFunction : ISharedFunction
    {
        private readonly IWebHostEnvironment _env;
        public SharedFunction(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void ThrowExceptionIfNull<T>(T item, string errorMessage)
        {
            if (item == null)
            {
                throw new Exception(errorMessage);
            }
        }
        public List<GetOvumFreezeSummaryDto> GetOvumDetailInfos(IQueryable<OvumDetail> ovumDetails)
        {
            var q = ovumDetails.Select(x => new GetOvumFreezeSummaryDto
            {
                courseOfTreatmentSqlId = x.CourseOfTreatment.SqlId,
                courseOfTreatmentId = x.CourseOfTreatmentId,
                ovumFromCourseOfTreatmentSqlId = x.OvumFromCourseOfTreatment.SqlId,
                ovumFromCourseOfTreatmentId = x.OvumFromCourseOfTreatmentId,
                ovumSource = x.CourseOfTreatment.OvumSource.Name,
                ovumSourceOwner = new BaseCustomerInfoDto
                {
                    customerSqlId = x.OvumFromCourseOfTreatment.Customer.SqlId,
                    customerId = x.OvumFromCourseOfTreatment.CustomerId,
                    customerName = x.OvumFromCourseOfTreatment.Customer.Name
                },
                ovumDetailId = x.OvumDetailId,
                ovumNumber = x.OvumNumber,
                ovumPickupTime = x.OvumPickup == null ? null : x.OvumPickup.StartTime,
                freezeTime = x.OvumFreeze == null ? null : x.OvumFreeze.FreezeTime,
                thawTime = x.OvumThaw == null ? null : x.OvumThaw.ThawTime,
                freezeObservationNoteInfo = x.ObservationNotes.Where(y => y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation && !y.IsDeleted).OrderByDescending(y => y.ObservationTime).Select(y => new GetObservationNoteNameDto
                {
                    ovumDetailId = y.OvumDetailId,
                    observationNoteId = y.ObservationNoteId,
                    day = y.Day,
                    memo = y.Memo,
                    ovumMaturationName = y.OvumMaturation.Name,
                    fertilizationResultName = y.FertilizationResult.Name,
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
                isThawed = x.OvumThawFreezePairFreezeOvumDetails.Count == 0 ? false : true,
            }).OrderBy(x => x.ovumPickupTime).ThenBy(x => x.ovumNumber).ToList();

            List<GetOvumFreezeSummaryDto> result = q.Where(x => !x.isThawed).ToList();
            return result;
        }
        public void ConvertPhotoToBase64String(List<GetOvumFreezeSummaryDto> result)
        {
            foreach (var i in result)
            {
                if (i.freezeObservationNoteInfo != null && i.freezeObservationNoteInfo.observationNotePhotos != null && i.freezeObservationNoteInfo.observationNotePhotos.Count > 0 && !string.IsNullOrEmpty(i.freezeObservationNoteInfo.observationNotePhotos[0].photoName))
                {
                    string path = Path.Combine(_env.ContentRootPath, "uploads", "images", i.freezeObservationNoteInfo.observationNotePhotos[0].photoName);
                    if (File.Exists(path))
                    {
                        i.freezeObservationNoteInfo.observationNotePhotos[0].imageBase64String = Convert.ToBase64String(File.ReadAllBytes(path));
                    }
                }
            }
        }
    }
}
