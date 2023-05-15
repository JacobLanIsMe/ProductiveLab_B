using Microsoft.EntityFrameworkCore;
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
        private async Task<List<GetOvumFreezeSummaryDto>> GetOvumDetailInfos(IQueryable<OvumDetail> ovumDetails)
        {
            var q = await ovumDetails.Select(x => new GetOvumFreezeSummaryDto
            {
                courseOfTreatmentSqlId = x.CourseOfTreatment.SqlId,
                courseOfTreatmentId = x.CourseOfTreatmentId,
                ovumFromCourseOfTreatmentSqlId = x.OvumFromCourseOfTreatment.SqlId,
                ovumFromCourseOfTreatmentId = x.OvumFromCourseOfTreatmentId,
                ovumSource = x.CourseOfTreatment.Treatment.OvumSource.Name,
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
                freezeObservationNoteInfo = x.ObservationNotes.Where(y => y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation).OrderByDescending(y => y.ObservationTime).Select(y => new GetObservationNoteNameDto
                {
                    ovumDetailId = y.OvumDetailId,
                    observationNoteId = y.ObservationNoteId,
                    day = y.Day,
                    memo = y.Memo,
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
            }).OrderBy(x => x.ovumPickupTime).ThenBy(x => x.ovumNumber).ToListAsync();

            List<GetOvumFreezeSummaryDto> result = q.Where(x => !x.isThawed).ToList();
            return result;
        }
        public async Task<List<GetOvumFreezeSummaryDto>> GetOvumFreezeSummary(Guid courseOfTreatmentId)
        {
            var customer = dbContext.CourseOfTreatments.FirstOrDefault(x => x.CourseOfTreatmentId == courseOfTreatmentId);
            if (customer == null)
            {
                return new List<GetOvumFreezeSummaryDto>();
            }
            Guid customerId = customer.CustomerId;
            var query = dbContext.OvumDetails.Where(x => x.CourseOfTreatment.CustomerId == customerId && x.OvumFreezeId != null && x.OvumThawFreezePairFreezeOvumDetails.Count == 0 && x.OvumTransferPairDonorOvumDetails.Count == 0);
            List<GetOvumFreezeSummaryDto> result = await GetOvumDetailInfos(query);
            ConvertPhotoToBase64String(result);
            return result;
        }
        public async Task<List<GetSpermFreezeSummaryDto>> GetSpermFreezeSummary(Guid courseOfTreatmentId)
        {
            var customer = await dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new
            {
                customerId = x.Treatment.SpermOperationId == (int)GermCellOperationEnum.freeze ? x.CustomerId : x.Customer.Spouse
            }).FirstOrDefaultAsync();
            if (customer == null || customer.customerId == null)
            {
                return new List<GetSpermFreezeSummaryDto>();
            }
            var result = await dbContext.SpermFreezes.Where(x => x.CourseOfTreatment.CustomerId == customer.customerId && !x.SpermThawFreezePairs.Any()).Select(x => new GetSpermFreezeSummaryDto
            {
                spermSource = x.CourseOfTreatment.Treatment.SpermSource.Name,
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
            }).OrderBy(x => x.courseOfTreatmentSqlId).ThenBy(x => x.vialNumber).ToListAsync();
            return result;
        }
        public async Task<List<GetOvumFreezeSummaryDto>> GetRecipientOvumFreezes(Guid courseOfTreatmentId)
        {
            var customer = dbContext.CourseOfTreatments.FirstOrDefault(x => x.CourseOfTreatmentId == courseOfTreatmentId);
            if (customer == null)
            {
                return new List<GetOvumFreezeSummaryDto>();
            }
            Guid customerId = customer.CustomerId;
            var ovumDetails = dbContext.OvumDetails.Where(x => x.CourseOfTreatment.CustomerId == customerId && x.FertilizationId == null && x.OvumFreezeId != null);

            var result = await GetOvumDetailInfos(ovumDetails);
            ConvertPhotoToBase64String(result);
            return result;
        }

        public async Task<List<GetOvumFreezeSummaryDto>> GetDonorOvums(int customerSqlId)
        {
            var customer = dbContext.Customers.FirstOrDefault(x => x.SqlId == customerSqlId);
            if (customer == null)
            {
                return new List<GetOvumFreezeSummaryDto>();
            }
            Guid customerId = customer.CustomerId;
            var ovumDetails = dbContext.OvumDetails.Where(x => x.CourseOfTreatment.CustomerId == customerId && x.FertilizationId == null && x.CourseOfTreatment.Treatment.OvumSourceId == (int)GermCellSourceEnum.OD && x.OvumTransferPairDonorOvumDetails.Count <= 0);
            var result = await GetOvumDetailInfos(ovumDetails);
            ConvertPhotoToBase64String(result);
            return result;
        }

        public async Task<List<GetOvumFreezeSummaryDto>> GetEmbryoFreezes(Guid courseOfTreatmentId)
        {
            var courseOfTreatment = dbContext.CourseOfTreatments.FirstOrDefault(x => x.CourseOfTreatmentId == courseOfTreatmentId);
            if (courseOfTreatment == null)
            {
                return new List<GetOvumFreezeSummaryDto>();
            }
            Guid customerId = courseOfTreatment.CustomerId;
            var ovumDetails = dbContext.OvumDetails.Where(x => x.CourseOfTreatment.CustomerId == customerId && x.OvumFreezeId != null && x.FertilizationId != null);
            var result = await GetOvumDetailInfos(ovumDetails);
            ConvertPhotoToBase64String(result);
            return result;
        }
        public async Task<List<Guid>> GetUnFreezingObservationNoteOvumDetails(List<Guid> ovumDetailIds)
        {
            var freezingObservationNoteOvumDetails = await dbContext.ObservationNotes.Where(x => ovumDetailIds.Contains(x.OvumDetailId) && x.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation).Select(x => x.OvumDetailId).ToListAsync();
            var unFreezingObservationNoteOvumDetails = ovumDetailIds.Where(x => !freezingObservationNoteOvumDetails.Contains(x)).ToList();
            return unFreezingObservationNoteOvumDetails;
        }
        
    }
}
