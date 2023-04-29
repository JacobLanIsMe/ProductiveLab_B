using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForTreatment;
using prjProductiveLab_B.Enums;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;
using System.Transactions;

namespace prjProductiveLab_B.Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly ReproductiveLabContext dbContext;
        private readonly ISharedFunctionService sharedFunction;
        public TreatmentService(ReproductiveLabContext dbContext, ISharedFunctionService sharedFunction)
        {
            this.dbContext = dbContext;
            this.sharedFunction = sharedFunction;
        }
        public BaseResponseDto AddOvumPickupNote(AddOvumPickupNoteDto ovumPickupNote)
        {
            BaseResponseDto result = new BaseResponseDto();
            string errorMessage = OvumPickupNoteValidation(ovumPickupNote);
            if (string.IsNullOrEmpty(errorMessage))
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        OvumPickup ovumPickup = new OvumPickup()
                        {
                            CourseOfTreatmentId = Guid.Parse(ovumPickupNote.courseOfTreatmentId),
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
                        sharedFunction.SetMediumInUse<OvumPickup>(ovumPickup, ovumPickupNote.mediumInUse);
                        dbContext.OvumPickups.Add(ovumPickup);
                        dbContext.SaveChanges();
                        Guid latestOvumPickupId = dbContext.OvumPickups.OrderByDescending(x=>x.SqlId).Select(x=>x.OvumPickupId).FirstOrDefault();
                        int ovumTotalNumber = ovumPickupNote.ovumPickupNumber.coc_Grade1 + ovumPickupNote.ovumPickupNumber.coc_Grade2 + ovumPickupNote.ovumPickupNumber.coc_Grade3 + ovumPickupNote.ovumPickupNumber.coc_Grade4 + ovumPickupNote.ovumPickupNumber.coc_Grade5;
                        for (int i = 1; i<=ovumTotalNumber; i++)
                        {
                            OvumPickupDetail ovumPickupDetail = new OvumPickupDetail()
                            {
                                OvumPickupId = latestOvumPickupId,
                                OvumNumber = i,
                                OvumPickupDetailStatusId = 1
                            };
                            dbContext.Add(ovumPickupDetail);
                        }
                        dbContext.SaveChanges();
                        scope.Complete();
                        
                    }
                    result.SetSuccess();
                }
                catch(TransactionAbortedException ex)
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
        // OvumPickupNote 表格的驗證，若有錯誤回傳錯誤訊息，若無錯誤回傳空字串。
        public string OvumPickupNoteValidation(AddOvumPickupNoteDto ovumPickupNote)
        {
            string errorMessage = "";
            if (!Guid.TryParse(ovumPickupNote.courseOfTreatmentId, out Guid transformedCourseId))
            {
                errorMessage += "會員有誤，請重新選擇客戶。\n";
            }
            if (ovumPickupNote.operationTime == null)
            {
                errorMessage += "操作時間有誤。\n";
            }
            if (ovumPickupNote.ovumPickupNumber == null)
            {
                errorMessage += "取卵結果有誤。\n";
            }
            else
            {
                int coc_grade5 = ovumPickupNote.ovumPickupNumber.coc_Grade5;
                int coc_grade4 = ovumPickupNote.ovumPickupNumber.coc_Grade4;
                int coc_grade3 = ovumPickupNote.ovumPickupNumber.coc_Grade3;
                int coc_grade2 = ovumPickupNote.ovumPickupNumber.coc_Grade2;
                int coc_grade1 = ovumPickupNote.ovumPickupNumber.coc_Grade1;
                if ((coc_grade1 + coc_grade2 + coc_grade3 + coc_grade4 + coc_grade5) != ovumPickupNote.ovumPickupNumber.totalOvumNumber)
                {
                    errorMessage += "取卵數總和有誤。\n";
                }
            }
            if (!Guid.TryParse(ovumPickupNote.embryologist, out Guid embryologist))
            {
                errorMessage += "胚胎師選項有誤。\n";
            }
            if (ovumPickupNote.mediumInUse == null || ovumPickupNote.mediumInUse.Count <= 0)
            {
                errorMessage += "培養液資訊有誤。\n";
            }
            return errorMessage;
        }

        public async Task<BaseTreatmentInfoDto> GetBaseTreatmentInfo(Guid courseOfTreatmentId)
        {
            var result = await dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new BaseTreatmentInfoDto
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
                    treatmentId = x.TreatmentId,
                    ovumSituationName = x.Treatment.OvumSituation.Name,
                    ovumSourceName = x.Treatment.OvumSource.Name,
                    ovumOperationName = x.Treatment.OvumOperation.Name,
                    spermSituationName = x.Treatment.SpermSituation.Name,
                    spermSourceName = x.Treatment.SpermSource.Name,
                    spermOperationName = x.Treatment.SpermOperation.Name,
                    spermRetrievalMethodName = x.Treatment.SpermRetrievalMethod.Name,
                    embryoSituationName = x.Treatment.EmbryoSituation.Name,
                    embryoOperationName = x.Treatment.EmbryoOperation.Name
                },
                memo = x.Memo
            }).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<TreatmentSummaryDto>> GetTreatmentSummary(Guid courseOfTreatmentId)
        {
            return await dbContext.OvumPickupDetails.Where(x => x.OvumPickup.CourseOfTreatmentId == courseOfTreatmentId || x.OvumThaw.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new TreatmentSummaryDto
            {
                ovumPickupDetailId = x.OvumPickupDetailId,
                courseOfTreatmentSqlId = x.OvumPickup != null ? x.OvumPickup.CourseOfTreatment.SqlId : x.OvumThaw.CourseOfTreatment.SqlId,
                ovumFromCourseOfTreatmentSqlId = x.OvumPickup != null ? x.OvumPickup.CourseOfTreatment.OvumFromCourseOfTreatment.SqlId : x.OvumThaw.CourseOfTreatment.OvumFromCourseOfTreatment.SqlId,
                ovumPickupDetailStatus = x.OvumPickupDetailStatus.Name,
                dateOfEmbryo = x.OvumPickup != null ? (DateTime.Now.Date - x.OvumPickup.CourseOfTreatment.SurgicalTime.Date).Days : dbContext.ObservationNotes.Where(y=>y.OvumPickupDetailId == x.OvumThawFreezePairThawOvumPickupDetails.Select(z=>z.FreezeOvumPickupDetailId).FirstOrDefault() && y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation && y.OvumPickupDetail.Fertilisation == null).Select(y=>y.Day).FirstOrDefault() + (DateTime.Now.Date - x.OvumThaw.ThawTime.Date).Days, 
                ovumNumber = x.OvumNumber,
                hasFertilization = x.Fertilisation == null ? false : true,
                observationNote = dbContext.ObservationNotes.Where(y => y.OvumPickupDetailId == x.OvumPickupDetailId).OrderByDescending(y => y.SqlId).Select(y => y.Memo).FirstOrDefault(),
                ovumSource = x.OvumPickup != null ? x.OvumPickup.CourseOfTreatment.OvumFromCourseOfTreatment.Treatment.OvumSource.Name : x.OvumThaw.CourseOfTreatment.OvumFromCourseOfTreatment.Treatment.OvumSource.Name

            }).OrderBy(x=>x.ovumNumber).AsNoTracking().ToListAsync();
        }

        public async Task<List<TreatmentDto>> GetAllTreatment()
        {
            var treatments = await dbContext.Treatments.Select(x => new TreatmentDto
            {
                treatmentId = x.SqlId,
                ovumSituationName = x.OvumSituation.Name,
                ovumSourceName = x.OvumSource.Name,
                ovumOperationName = x.OvumOperation.Name,
                spermSituationName = x.SpermSituation.Name,
                spermSourceName = x.SpermSource.Name,
                spermOperationName = x.SpermOperation.Name,
                spermRetrievalMethodName = x.SpermRetrievalMethod.Name,
                embryoSituationName = x.EmbryoSituation.Name,
                embryoOperationName = x.EmbryoOperation.Name
            }).OrderBy(x => x.treatmentId).AsNoTracking().ToListAsync();
            return treatments;
        }

        public async Task<BaseResponseDto> AddCourseOfTreatment(AddCourseOfTreatmentDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    CourseOfTreatment course = new CourseOfTreatment
                    {
                        Doctor = input.doctorId,
                        CustomerId = input.customerId,
                        TreatmentId = input.treatmentId,
                        SurgicalTime = input.surgicalTime,
                        TreatmentStatusId = 1,
                        Memo = input.memo,
                    };
                    dbContext.CourseOfTreatments.Add(course);
                    dbContext.SaveChanges();
                    scope.Complete();
                }
                result.SetSuccess();
            }
            catch(Exception ex)
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
                using(TransactionScope scope = new TransactionScope())
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
                        TopColorId = input.topColorId
                    };
                    dbContext.OvumFreezes.Add(ovumFreeze);
                    dbContext.SaveChanges();
                    Guid latestOvumFreezeId = dbContext.OvumFreezes.OrderByDescending(x=>x.SqlId).Select(x=>x.OvumFreezeId).FirstOrDefault();
                    var ovumPickupDetails = dbContext.OvumPickupDetails.Where(x => input.ovumPickupDetailId.Contains(x.OvumPickupDetailId));
                    foreach(var i in ovumPickupDetails)
                    {
                        i.OvumPickupDetailStatusId = (int)OvumPickupDetailStatusEnum.Freeze;
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
            catch(Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }
        private void AddOvumFreezeValidation(AddOvumFreezeDto input)
        {
            if (input.ovumPickupDetailId.Count <= 0 || input.ovumPickupDetailId.Count > 4)
            {
                throw new Exception("卵子數量請介於 1-4");
            }
            var isFreezed = dbContext.OvumPickupDetails.FirstOrDefault(x => input.ovumPickupDetailId.Contains(x.OvumPickupDetailId) && x.OvumPickupDetailStatusId == (int)OvumPickupDetailStatusEnum.Freeze);
            if (isFreezed != null)
            {
                throw new Exception($"卵子編號: {isFreezed.OvumNumber} 已冷凍入庫");
            }
            var hasFreezeObservationNote = dbContext.OvumPickupDetails.Where(x => input.ovumPickupDetailId.Contains(x.OvumPickupDetailId)).Include(x => x.ObservationNotes).FirstOrDefault(x => !x.ObservationNotes.Any(y => y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation));
            if (hasFreezeObservationNote != null)
            {
                throw new Exception($"卵子編號: {hasFreezeObservationNote.OvumNumber} 尚無冷凍觀察紀錄");
            }
        }
        public async Task<Guid> GetOvumOwnerCustomerId(Guid courseOfTreatmentId)
        {
            Guid customerId = default(Guid);
            var courseOfTreatment = await dbContext.CourseOfTreatments.FirstOrDefaultAsync(x => x.CourseOfTreatmentId == courseOfTreatmentId);
            if (courseOfTreatment == null)
            {
                return customerId;
            }
            if (courseOfTreatment.OvumFromCourseOfTreatmentId == courseOfTreatmentId)
            {
                customerId = courseOfTreatment.CustomerId;
            }
            else
            {
                customerId = await dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatment.OvumFromCourseOfTreatmentId).Select(x => x.CustomerId).FirstOrDefaultAsync();
            }
            return customerId;
        }

        public async Task<BaseCustomerInfoDto> GetOvumOwnerInfo(Guid courseOfTreatmentId)
        {
            Guid customerId = await GetOvumOwnerCustomerId(courseOfTreatmentId);
            if (customerId == default(Guid))
            {
                return new BaseCustomerInfoDto();
            }
            var result = await dbContext.Customers.Where(x=>x.CustomerId == customerId).Select(x=>new BaseCustomerInfoDto
            {
                birthday = x.Birthday,
                customerName = x.Name,
                customerSqlId = x.SqlId
            }).FirstOrDefaultAsync();
            if (result == null)
            {
                return new BaseCustomerInfoDto();
            }
            return result;
        }
        public async Task<List<CommonDto>> GetTopColors()
        {
            return await dbContext.TopColors.Select(x=>new CommonDto
            {
                id = x.SqlId,
                name = x.Name
            }).ToListAsync();
        }
        public async Task<List<CommonDto>> GetFertilisationMethods()
        {
            return await dbContext.FertilisationMethods.Select(x=>new CommonDto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x=>x.id).ToListAsync();
        }
        public async Task<List<CommonDto>> GetIncubators()
        {
            return await dbContext.Incubators.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).ToListAsync();
        }
        public BaseResponseDto AddFertilisation(AddFertilisationDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using(TransactionScope scope = new TransactionScope())
                {
                    Fertilisation fertilisation = new Fertilisation
                    {
                        FertilisationTime = input.fertilisationTime,
                        Embryologist = input.embryologist,
                        FertilisationMethodId = input.fertilisationMethodId,
                        IncubatorId = input.incubatorId,
                        OtherIncubator = input.otherIncubator
                    };
                    sharedFunction.SetMediumInUse(fertilisation, input.mediumInUseIds);
                    dbContext.Fertilisations.Add(fertilisation);
                    dbContext.SaveChanges();
                    Guid latestFertilisationId = dbContext.Fertilisations.OrderByDescending(x=>x.SqlId).Select(x=>x.FertilisationId).FirstOrDefault();
                    if (latestFertilisationId == Guid.Empty)
                    {
                        throw new Exception("Fertilisation 資料表寫入也誤");
                    }
                    var ovumPickupDetailIds = dbContext.OvumPickupDetails.Where(x=>input.ovumPickupDetailIds.Contains(x.OvumPickupDetailId)).ToList();
                    foreach (var i in ovumPickupDetailIds)
                    {
                        i.FertilisationId = latestFertilisationId;
                    }
                    dbContext.SaveChanges();
                    scope.Complete();
                }
                result.SetSuccess();
            }
            catch(Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }

        //public BaseResponseDto AddOvumThaw()
        //{

        //}
    }
}
