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
                            OvumDetail ovumDetail = new OvumDetail()
                            {
                                CourseOfTreatmentId = ovumPickupNote.courseOfTreatmentId,
                                OvumPickupId = latestOvumPickupId,
                                OvumNumber = i,
                                OvumDetailStatusId = (int)OvumDetailStatusEnum.Incubation
                            };
                            dbContext.Add(ovumDetail);
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
            return await dbContext.OvumDetails.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new TreatmentSummaryDto
            {
                ovumDetailId = x.OvumDetailId,
                courseOfTreatmentSqlId = x.CourseOfTreatment.SqlId,
                ovumFromCourseOfTreatmentSqlId = x.CourseOfTreatment.OvumFromCourseOfTreatment.SqlId,
                ovumDetailStatus = x.OvumDetailStatus.Name,
                dateOfEmbryo = x.OvumPickupId != null ? (DateTime.Now.Date - x.CourseOfTreatment.SurgicalTime.Date).Days : dbContext.ObservationNotes.Where(y=>y.OvumDetailId == x.OvumThawFreezePairThawOvumDetails.Select(z=>z.FreezeOvumDetailId).FirstOrDefault() && y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation).Select(y=>y.Day).FirstOrDefault() + (DateTime.Now.Date - x.OvumThaw.ThawTime.Date).Days, 
                ovumNumber = x.OvumNumber,
                hasFertilization = x.FertilisationId == null ? false : true,
                observationNote = x.ObservationNotes.OrderByDescending(y => y.SqlId).Select(y => y.Memo).FirstOrDefault(),
                ovumSource = x.CourseOfTreatment.OvumFromCourseOfTreatment.Treatment.OvumSource.Name

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

        public async Task<List<BaseCustomerInfoDto>> GetAllCustomer()
        {
            return await dbContext.Customers.Select(x => new BaseCustomerInfoDto
            {
                customerId = x.CustomerId,
                customerSqlId = x.SqlId,
                customerName = x.Name,
                birthday = x.Birthday
            }).OrderBy(x => x.customerSqlId).ToListAsync();
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
                        TopColorId = input.topColorId,
                        IsThawed = false
                    };
                    dbContext.OvumFreezes.Add(ovumFreeze);
                    dbContext.SaveChanges();
                    Guid latestOvumFreezeId = dbContext.OvumFreezes.OrderByDescending(x=>x.SqlId).Select(x=>x.OvumFreezeId).FirstOrDefault();
                    var ovumDetails = dbContext.OvumDetails.Where(x => input.ovumDetailId.Contains(x.OvumDetailId));
                    foreach(var i in ovumDetails)
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
            catch(Exception ex)
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
            var isFreezed = dbContext.OvumDetails.FirstOrDefault(x => input.ovumDetailId.Contains(x.OvumDetailId) && x.OvumDetailStatusId == (int)OvumDetailStatusEnum.Freeze);
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
                    var ovumDetailIds = dbContext.OvumDetails.Where(x=>input.ovumDetailIds.Contains(x.OvumDetailId)).ToList();
                    foreach (var i in ovumDetailIds)
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

        public BaseResponseDto AddOvumThaw(AddOvumThawDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                AddOvumThawValidation(input);
                using(TransactionScope scope = new TransactionScope())
                {
                    OvumThaw ovumThaw = new OvumThaw
                    {
                        ThawTime = input.thawTime,
                        ThawMediumInUseId = input.thawMediumInUseId,
                        Embryologist = input.embryologist,
                        RecheckEmbryologist = input.recheckEmbryologist
                    };
                    sharedFunction.SetMediumInUse<OvumThaw>(ovumThaw, input.mediumInUseIds);
                    dbContext.OvumThaws.Add(ovumThaw);
                    dbContext.SaveChanges();
                    Guid latestOvumThawId = dbContext.OvumThaws.OrderByDescending(x=>x.SqlId).Select(x=>x.OvumThawId).FirstOrDefault();
                    for (int i = 0; i < input.freezeOvumDetailIds.Count; i++)
                    {
                        OvumDetail ovumDetail = new OvumDetail
                        {
                            CourseOfTreatmentId = input.courseOfTreatmentId,
                            OvumNumber = i + 1,
                            OvumDetailStatusId = (int)OvumDetailStatusEnum.Incubation,
                            OvumThawId = latestOvumThawId,
                        };
                        dbContext.OvumDetails.Add(ovumDetail);
                        dbContext.SaveChanges();
                        Guid thawOvumDetailId = dbContext.OvumDetails.OrderByDescending(x=>x.SqlId).Select(x=>x.OvumDetailId).FirstOrDefault();
                        OvumThawFreezePair ovumThawFreezePair = new OvumThawFreezePair
                        {
                            FreezeOvumDetailId = input.freezeOvumDetailIds[i],
                            ThawOvumDetailId = thawOvumDetailId
                        };
                        dbContext.OvumThawFreezePairs.Add(ovumThawFreezePair);
                        dbContext.SaveChanges();
                    }
                    var q = dbContext.OvumDetails.Where(x => x.OvumFreezeId != null && input.freezeOvumDetailIds.Contains(x.OvumDetailId)).Select(x => new
                    {
                        ovumFreeze = x.OvumFreeze,
                        storageUnit = x.OvumFreeze.StorageUnit
                    });
                    foreach (var i in q)
                    {
                        i.ovumFreeze.IsThawed = true;
                        i.storageUnit.IsOccupied = false;
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
        }

        public async Task<BaseResponseDto> OvumBankTransfer(OvumBankTransferDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                if (input.transferOvumDetailIds == null || input.transferOvumDetailIds.Count < 1)
                {
                    throw new Exception("請選擇要轉移的卵子");
                }
                using(TransactionScope scope = new TransactionScope())
                {
                    // 將受贈者的OvumFromCourseOfTreatmentId 改成捐贈者的 CourseOfTreatmentId
                    var recipientCourseOfTreatment = dbContext.CourseOfTreatments.Where(x => x.SqlId == input.recipientCourseOfTreatmentSqlId).Select(x => new
                    {
                        recipientCourseOfTreatment = x,
                        ovumSourceId = x.Treatment.OvumSourceId
                    }).FirstOrDefault();
                    if (recipientCourseOfTreatment == null)
                    {
                        throw new Exception("查無此受贈者之療程編號");
                    }
                    recipientCourseOfTreatment.recipientCourseOfTreatment.OvumFromCourseOfTreatmentId = input.donorCourseOfTreatmentId;

                    var donorOvumDetails = dbContext.OvumDetails.Where(x => input.transferOvumDetailIds.Contains(x.OvumDetailId)).ToList();
                    if (donorOvumDetails.Count != input.transferOvumDetailIds.Count)
                    {
                        throw new Exception("捐贈卵子資訊有誤");
                    }
                    for (int i = 0; i < input.transferOvumDetailIds.Count; i++)
                    {
                        // Donor 的 OvumDetail 的 IsTransferred 欄位改為 true
                        var donorOvumDetail = dbContext.OvumDetails.FirstOrDefault(x => x.OvumDetailId == donorOvumDetails[i].OvumDetailId);
                        if (donorOvumDetail == null)
                        {
                            throw new Exception("捐贈卵子資訊有誤");
                        }
                        donorOvumDetail.IsTransferred = true;
                        // 加入新的 OvumDetail 資料
                        OvumDetail ovumDetail = new OvumDetail
                        {
                            OvumNumber = i + 1,
                            OvumFreezeId = donorOvumDetails[i].OvumFreezeId,
                            CourseOfTreatmentId = recipientCourseOfTreatment.recipientCourseOfTreatment.CourseOfTreatmentId
                        };
                        if (recipientCourseOfTreatment.ovumSourceId == (int)GermCellSourceEnum.OR)
                        {
                            ovumDetail.OvumDetailStatusId = (int)OvumDetailStatusEnum.Freeze;
                        }
                        if (recipientCourseOfTreatment.ovumSourceId == (int)GermCellSourceEnum.OR_F)
                        {
                            ovumDetail.OvumDetailStatusId = (int)OvumDetailStatusEnum.Incubation;
                        }
                        dbContext.OvumDetails.Add(ovumDetail);
                        dbContext.SaveChanges();
                        Guid latestOvumDetailId = dbContext.OvumDetails.OrderByDescending(x => x.SqlId).Select(x=>x.OvumDetailId).FirstOrDefault();
                        // 加入新的 OvumTransferPair 資料
                        OvumTransferPair pair = new OvumTransferPair
                        {
                            RecipientOvumDetailId = latestOvumDetailId,
                            DonorOvumDetailId = donorOvumDetail.OvumDetailId
                        };
                        dbContext.OvumTransferPairs.Add(pair);

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
        
    }
}
