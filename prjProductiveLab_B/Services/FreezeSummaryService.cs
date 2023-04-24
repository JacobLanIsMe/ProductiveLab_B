using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Enums;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;

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
        public async Task<List<GetOvumFreezeSummaryDto>> GetOvumFreezeSummarys(Guid courseOfTreatmentId) 
        {
            var customerId = dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => x.CustomerId).FirstOrDefault();
            var result = await dbContext.OvumPickupDetails.Where(x => x.OvumPickup.CourseOfTreatment.CustomerId == customerId && x.OvumFreezeId != null).Select(x => new GetOvumFreezeSummaryDto
            {
                courseOfTreatmentSqlId = x.OvumPickup.CourseOfTreatment.SqlId,
                courseOfTreatmentId = x.OvumPickup.CourseOfTreatmentId,
                ovumFromCourseOfTreatmentSqlId = dbContext.CourseOfTreatments.Where(y=>y.CourseOfTreatmentId == x.OvumPickup.CourseOfTreatment.OvumFromCourseOfTreatmentId).Select(y=>y.SqlId).FirstOrDefault(),
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

            }).OrderBy(x=>x.ovumPickupTime).ThenBy(x=>x.ovumNumber).ToListAsync();
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
            return result;
            
        }
    }
}
