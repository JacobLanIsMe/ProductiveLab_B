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
        public async Task<GetOvumFreezeSummaryDto> GetOvumFreezeSummary(Guid courseOfTreatmentId) 
        {
            var customerId = dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => x.CustomerId).FirstOrDefault();

            var result = await dbContext.OvumPickupDetails.Where(x => x.OvumPickup.CourseOfTreatment.CustomerId == customerId && x.OvumFreeze != null).Select(x => new GetOvumFreezeSummaryDto
            {
                courseOfTreatmentId = x.OvumPickup.CourseOfTreatmentId,
                ovumFromCourseOfTreatmentId = x.OvumPickup.CourseOfTreatment.OvumFromCourseOfTreatmentId,
                ovumNumber = x.OvumNumber,
                ovumPickupTime = x.OvumPickup.CourseOfTreatment.SurgicalTime,
                freezeTime = x.OvumFreeze.FreezeTime,
                freezeObservationNoteInfo = dbContext.ObservationNotes.Where(y => y.OvumPickupDetailId == x.OvumPickupDetailId && y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation).Include(y => y.ObservationNotePhotos).OrderByDescending(y => y.ObservationTime).Select(y => new FreezeObservationNote
                {
                    day = y.Day,
                    memo = y.Memo,
                    freezeObservationNoteMainPhotoName = y.ObservationNotePhotos.Where(z => z.IsMainPhoto == true && z.IsDeleted == false).Select(z => z.PhotoName).FirstOrDefault()
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

            }).FirstOrDefaultAsync();
            if (result == null)
            {
                return new GetOvumFreezeSummaryDto();
            }
            if (result.freezeObservationNoteInfo.freezeObservationNoteMainPhotoName != null)
            {
                string path = Path.Combine(env.ContentRootPath, "uploads", "images", result.freezeObservationNoteInfo.freezeObservationNoteMainPhotoName);
                if (File.Exists(path))
                {
                    result.freezeObservationNoteInfo.photoBase64String = Convert.ToBase64String(File.ReadAllBytes(path));
                }
            }
            return result;
            
        }
    }
}
