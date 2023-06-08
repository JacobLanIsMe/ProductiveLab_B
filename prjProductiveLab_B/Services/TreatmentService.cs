//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using prjProductiveLab_B.Dtos;
//using prjProductiveLab_B.Dtos.ForObservationNote;
//using prjProductiveLab_B.Dtos.ForStorage;
//using prjProductiveLab_B.Dtos.ForTreatment;
//using prjProductiveLab_B.Enums;
//using prjProductiveLab_B.Interfaces;
//using ReproductiveLabDB.Models;
//using System.Text;
//using System.Transactions;

//namespace prjProductiveLab_B.Services
//{
//    public class TreatmentService : ITreatmentService
//    {
//        private readonly ReproductiveLabContext dbContext;
//        private readonly ISharedFunctionService sharedFunction;
//        private readonly IObservationNoteService observationNoteService;

//        public TreatmentService(ReproductiveLabContext dbContext, ISharedFunctionService sharedFunction, IObservationNoteService observationNoteService)
//        {
//            this.dbContext = dbContext;
//            this.sharedFunction = sharedFunction;
//            this.observationNoteService = observationNoteService;
//        }
//        public BaseResponseDto AddOvumPickupNote(AddOvumPickupNoteDto ovumPickupNote)
//        {
//            BaseResponseDto result = new BaseResponseDto();
//            string errorMessage = OvumPickupNoteValidation(ovumPickupNote);
//            if (string.IsNullOrEmpty(errorMessage))
//            {
//                try
//                {
//                    using (TransactionScope scope = new TransactionScope())
//                    {
//                        OvumPickup ovumPickup = new OvumPickup()
//                        {
//                            TriggerTime = (DateTime)ovumPickupNote.operationTime.triggerTime,
//                            StartTime = (DateTime)ovumPickupNote.operationTime.startTime,
//                            EndTime = (DateTime)ovumPickupNote.operationTime.endTime,
//                            CocGrade5 = ovumPickupNote.ovumPickupNumber.coc_Grade5,
//                            CocGrade4 = ovumPickupNote.ovumPickupNumber.coc_Grade4,
//                            CocGrade3 = ovumPickupNote.ovumPickupNumber.coc_Grade3,
//                            CocGrade2 = ovumPickupNote.ovumPickupNumber.coc_Grade2,
//                            CocGrade1 = ovumPickupNote.ovumPickupNumber.coc_Grade1,
//                            Embryologist = Guid.Parse(ovumPickupNote.embryologist),
//                            UpdateTime = DateTime.Now
//                        };
//                        sharedFunction.SetMediumInUse<OvumPickup>(ovumPickup, ovumPickupNote.mediumInUse);
//                        dbContext.OvumPickups.Add(ovumPickup);
//                        dbContext.SaveChanges();
//                        Guid latestOvumPickupId = dbContext.OvumPickups.OrderByDescending(x=>x.SqlId).Select(x=>x.OvumPickupId).FirstOrDefault();
//                        int ovumTotalNumber = ovumPickupNote.ovumPickupNumber.coc_Grade1 + ovumPickupNote.ovumPickupNumber.coc_Grade2 + ovumPickupNote.ovumPickupNumber.coc_Grade3 + ovumPickupNote.ovumPickupNumber.coc_Grade4 + ovumPickupNote.ovumPickupNumber.coc_Grade5;
//                        for (int i = 1; i<=ovumTotalNumber; i++)
//                        {
//                            OvumDetail ovumDetail = new OvumDetail()
//                            {
//                                CourseOfTreatmentId = ovumPickupNote.courseOfTreatmentId,
//                                OvumFromCourseOfTreatmentId = ovumPickupNote.courseOfTreatmentId,
//                                OvumPickupId = latestOvumPickupId,
//                                OvumNumber = i,
//                                OvumDetailStatusId = (int)OvumDetailStatusEnum.Incubation
//                            };
//                            dbContext.Add(ovumDetail);
//                        }
//                        dbContext.SaveChanges();
//                        scope.Complete();
                        
//                    }
//                    result.SetSuccess();
//                }
//                catch(TransactionAbortedException ex)
//                {
//                    result.SetError(ex.Message);
//                }
//            }
//            else
//            {
//                result.SetError(errorMessage);
//            }
//            return result;



//        }
//        // OvumPickupNote 表格的驗證，若有錯誤回傳錯誤訊息，若無錯誤回傳空字串。
//        public string OvumPickupNoteValidation(AddOvumPickupNoteDto ovumPickupNote)
//        {
//            string errorMessage = "";
//            if (ovumPickupNote.operationTime == null)
//            {
//                errorMessage += "操作時間有誤。\n";
//            }
//            if (ovumPickupNote.ovumPickupNumber == null)
//            {
//                errorMessage += "取卵結果有誤。\n";
//            }
//            else
//            {
//                int coc_grade5 = ovumPickupNote.ovumPickupNumber.coc_Grade5;
//                int coc_grade4 = ovumPickupNote.ovumPickupNumber.coc_Grade4;
//                int coc_grade3 = ovumPickupNote.ovumPickupNumber.coc_Grade3;
//                int coc_grade2 = ovumPickupNote.ovumPickupNumber.coc_Grade2;
//                int coc_grade1 = ovumPickupNote.ovumPickupNumber.coc_Grade1;
//                if ((coc_grade1 + coc_grade2 + coc_grade3 + coc_grade4 + coc_grade5) != ovumPickupNote.ovumPickupNumber.totalOvumNumber)
//                {
//                    errorMessage += "取卵數總和有誤。\n";
//                }
//            }
//            if (!Guid.TryParse(ovumPickupNote.embryologist, out Guid embryologist))
//            {
//                errorMessage += "胚胎師選項有誤。\n";
//            }
//            if (ovumPickupNote.mediumInUse == null || ovumPickupNote.mediumInUse.Count <= 0)
//            {
//                errorMessage += "培養液資訊有誤。\n";
//            }
//            return errorMessage;
//        }

//        public async Task<BaseTreatmentInfoDto> GetBaseTreatmentInfo(Guid courseOfTreatmentId)
//        {
//            var result = await dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new BaseTreatmentInfoDto
//            {
//                courseOfTreatmentSqlId = x.SqlId,
//                customerSqlId = x.Customer.SqlId,
//                customerName = x.Customer.Name,
//                birthday = x.Customer.Birthday,
//                spouseSqlId = x.Customer.SpouseNavigation.SqlId,
//                spouseName = x.Customer.SpouseNavigation.Name,
//                doctor = x.DoctorNavigation.Name,
//                treatment = new TreatmentDto
//                {
//                    ovumSituationName = x.OvumSituation.Name,
//                    ovumSourceName = x.OvumSource.Name,
//                    ovumOperationName = x.OvumOperation.Name,
//                    spermSituationName = x.SpermSituation.Name,
//                    spermSourceName = x.SpermSource.Name,
//                    spermOperationName = x.SpermOperation.Name,
//                    spermRetrievalMethodName = x.SpermRetrievalMethod.Name,
//                    embryoSituationName = x.EmbryoSituation.Name,
//                    embryoOperationName = x.EmbryoOperation.Name
//                },
//                memo = x.Memo
//            }).AsNoTracking().FirstOrDefaultAsync();
//            return result;
//        }

//        public async Task<List<TreatmentSummaryDto>> GetTreatmentSummary(Guid courseOfTreatmentId)
//        {
//            var q = await dbContext.OvumDetails.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new
//            {
//                ovumDetailId = x.OvumDetailId,
//                courseOfTreatmentSqlId = x.CourseOfTreatment.SqlId,
//                ovumDetailStatus = x.OvumDetailStatus.Name,
//                ovumNumber = x.OvumNumber,
//                fertilizationTime = x.Fertilization == null ? new DateTime(1753, 1, 1, 0, 0, 0) : x.Fertilization.FertilizationTime,
//                fertilizationMethod = x.Fertilization == null ? null : x.Fertilization.FertilizationMethod.Name,
//                observationNoteId = x.OvumFreezeId == null ? x.ObservationNotes.Where(y=>!y.IsDeleted).OrderByDescending(y=>y.SqlId).Select(y=>y.ObservationNoteId).FirstOrDefault() : x.ObservationNotes.Where(y=>!y.IsDeleted && y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation).Select(y=>y.ObservationNoteId).FirstOrDefault(),
//                freezeStorageInfo = x.OvumFreeze == null ? null : new BaseStorage
//                {
//                    tankInfo = new StorageTankDto
//                    {
//                        tankName = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.StorageTank.TankName,
//                        tankTypeId = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.StorageTank.StorageTankTypeId
//                    },
//                    tankId = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.StorageTankId,
//                    canistId = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanistId,
//                    canistName = x.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.CanistName,
//                    stripBoxId = x.OvumFreeze.StorageUnit.StorageStripBoxId,
//                    stripBoxName = x.OvumFreeze.StorageUnit.StorageStripBox.StripBoxName,
//                    topColorName = x.OvumFreeze.TopColor.Name,
//                    unitInfo = new StorageUnitDto
//                    {
//                        storageUnitId = x.OvumFreeze.StorageUnitId,
//                        unitName = x.OvumFreeze.StorageUnit.UnitName,
//                        isOccupied = x.OvumFreeze.StorageUnit.IsOccupied
//                    }
//                },
//                ovumSource = x.CourseOfTreatment.OvumSource == null ? null : x.CourseOfTreatment.OvumSource.Name,
//                ovumFromCourseOfTreatmentSqlId = x.OvumFromCourseOfTreatment.SqlId,
//                hasPickup = x.OvumPickupId == null ? false : true,
//                isFreshPickup = x.OvumPickupId != null && x.OvumFreezeId == null ? true : false,
//                hasFreeze = x.OvumFreezeId != null ? true : false,
//                hasTransfer = x.OvumTransferPairRecipientOvumDetails.Any() ? true : false,
//                hasThaw = x.OvumThawFreezePairThawOvumDetails.Any() ? true : false,
//                isFreezeTransfer = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumThawId == null && x.OvumTransferPairRecipientOvumDetails.Any(y => y.DonorOvumDetail.OvumFreezeId != null) ? true : false,
//                isTransferThaw = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumThaw != null ? true : false,
//                isFreshTransfer = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumTransferPairRecipientOvumDetails.Any(y => y.DonorOvumDetail.OvumFreezeId == null) ? true : false,
//                day_FreshPickup = x.OvumPickupId != null && x.OvumFreezeId == null ? (DateTime.Now.Date - x.CourseOfTreatment.SurgicalTime.Date).Days : 0,
//                day_Freeze = x.OvumFreezeId != null ? x.ObservationNotes.Where(y => y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation && !y.IsDeleted).Select(y => y.Day).FirstOrDefault() : 0,
//                day_FreezeTransfer = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumThawId == null && x.OvumTransferPairRecipientOvumDetails.Any(y => y.DonorOvumDetail.OvumFreezeId != null) ? x.OvumTransferPairRecipientOvumDetails.Select(y => y.DonorOvumDetail.ObservationNotes.Where(z => z.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation && !z.IsDeleted).Select(z => z.Day).FirstOrDefault()).FirstOrDefault() : 0,
//                day_TransferThaw = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumThaw != null ? ((DateTime.Now.Date - x.OvumThaw.ThawTime.Date).Days + x.OvumTransferPairRecipientOvumDetails.Select(y => y.DonorOvumDetail.ObservationNotes.Where(z => z.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation && !z.IsDeleted).Select(z => z.Day).FirstOrDefault()).FirstOrDefault()) : 0,
//                day_FreshTransfer = x.OvumTransferPairRecipientOvumDetails.Any() && x.OvumTransferPairRecipientOvumDetails.Any(y => y.DonorOvumDetail.OvumFreezeId == null) ? (DateTime.Now.Date - x.OvumTransferPairRecipientOvumDetails.Select(y => y.DonorOvumDetail.CourseOfTreatment.SurgicalTime).FirstOrDefault().Date).Days : 0,
//                day_Thaw = x.OvumThawFreezePairThawOvumDetails.Any() && x.OvumThaw != null ? (DateTime.Now.Date - x.OvumThaw.ThawTime.Date).Days + x.OvumThawFreezePairThawOvumDetails.Select(y => y.FreezeOvumDetail.ObservationNotes.Where(z => z.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation && !z.IsDeleted).Select(z => z.Day).FirstOrDefault()).FirstOrDefault() : 0,
//            }).OrderBy(x=>x.ovumNumber).ToListAsync();

//            List<Guid> observationNoteIds = q.Select(x => x.observationNoteId).ToList();
//            var observationNotes = await observationNoteService.GetObservationNoteNameByObservationNoteIds(observationNoteIds);


//            List<TreatmentSummaryDto> result = new List<TreatmentSummaryDto>();
//            foreach (var i in q)
//            {
//                TreatmentSummaryDto treatment = new TreatmentSummaryDto
//                {
//                    ovumDetailId = i.ovumDetailId,
//                    courseOfTreatmentSqlId = i.courseOfTreatmentSqlId,
//                    ovumDetailStatus = i.ovumDetailStatus,
//                    ovumNumber = i.ovumNumber,
//                    fertilizationTime = i.fertilizationTime == new DateTime(1753, 1, 1, 0, 0, 0) ? null : i.fertilizationTime,
//                    fertilizationMethod = i.fertilizationMethod == null ? null : i.fertilizationMethod,
//                    observationNote = observationNotes.FirstOrDefault(x=>x.ovumDetailId == i.ovumDetailId),
//                    ovumFromCourseOfTreatmentSqlId = i.ovumFromCourseOfTreatmentSqlId,
//                    ovumSource = i.ovumSource,
//                    freezeStorageInfo = i.freezeStorageInfo
//                };
//                if (i.hasFreeze)
//                {
//                    treatment.dateOfEmbryo = i.day_Freeze;
//                }
//                else if (i.hasPickup)
//                {
//                    if (i.isFreshPickup)
//                    {
//                        treatment.dateOfEmbryo = i.day_FreshPickup;
//                    }
//                    else
//                    {
//                        treatment.dateOfEmbryo = default;
//                    }
//                }
//                else if (i.hasTransfer)
//                {
//                    if (i.isFreezeTransfer)
//                    {
//                        treatment.dateOfEmbryo = i.day_FreezeTransfer;
//                    }
//                    else if (i.isTransferThaw)
//                    {
//                        treatment.dateOfEmbryo = i.day_TransferThaw;
//                    }
//                    else if (i.isFreezeTransfer)
//                    {
//                        treatment.dateOfEmbryo = i.day_FreshTransfer;
//                    }
//                    else
//                    {
//                        treatment.dateOfEmbryo = default;
//                    }
//                }
//                else
//                {
//                    treatment.dateOfEmbryo = i.day_Thaw;
//                }
//                result.Add(treatment);
//            }
//            return result;
//        }

//        public async Task<List<CommonDto>> GetGermCellSituations()
//        {
//            return await dbContext.GermCellSituations.Select(x => new CommonDto
//            {
//                id = x.SqlId,
//                name = x.Name
//            }).ToListAsync();
//        }
//        public async Task<List<CommonDto>> GetGermCellSources()
//        {
//            return await dbContext.GermCellSources.Select(x => new CommonDto
//            {
//                id = x.SqlId,
//                name = x.Name
//            }).ToListAsync();
//        }
//        public async Task<List<CommonDto>> GetGermCellOperations()
//        {
//            return await dbContext.GermCellOperations.Select(x => new CommonDto
//            {
//                id = x.SqlId,
//                name = x.Name
//            }).ToListAsync();
//        }
//        public async Task<List<CommonDto>> GetSpermRetrievalMethods()
//        {
//            return await dbContext.SpermRetrievalMethods.Select(x => new CommonDto
//            {
//                id = x.SqlId,
//                name = x.Name
//            }).ToListAsync();
//        }
//        public async Task<List<BaseCustomerInfoDto>> GetAllCustomer()
//        {
//            return await dbContext.Customers.Select(x => new BaseCustomerInfoDto
//            {
//                customerId = x.CustomerId,
//                customerSqlId = x.SqlId,
//                customerName = x.Name,
//                birthday = x.Birthday
//            }).OrderBy(x => x.customerSqlId).ToListAsync();
//        }
//        public async Task<BaseCustomerInfoDto> GetCustomerByCustomerSqlId(int customerSqlId)
//        {
//            var result = await dbContext.Customers.Where(x=>x.SqlId == customerSqlId).Select(x=>new BaseCustomerInfoDto
//            {
//                customerId = x.CustomerId,
//                customerSqlId = x.SqlId,
//                customerName = x.Name,
//            }).FirstOrDefaultAsync();
//            if (result == null)
//            {
//                return new BaseCustomerInfoDto();
//            }
//            return result;
//        }
//        public async Task<BaseCustomerInfoDto> GetCustomerByCourseOfTreatmentId(Guid courseOfTreatmentId)
//        {
//            var result = await dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new BaseCustomerInfoDto
//            {
//                customerId = x.CustomerId,
//                customerSqlId = x.Customer.SqlId,
//                customerName = x.Customer.Name
//            }).FirstOrDefaultAsync();
//            if (result == null)
//            {
//                return new BaseCustomerInfoDto();
//            }
//            return result;
//        }
//        public async Task<BaseResponseDto> AddCourseOfTreatment(AddCourseOfTreatmentDto input)
//        {
//            BaseResponseDto result = new BaseResponseDto();
//            try
//            {
//                using (TransactionScope scope = new TransactionScope())
//                {
//                    CourseOfTreatment course = new CourseOfTreatment
//                    {
//                        Doctor = input.doctorId,
//                        CustomerId = input.customerId,
//                        SurgicalTime = input.surgicalTime,
//                        TreatmentStatusId = 1,
//                        Memo = input.memo,
//                    };
//                    if (int.TryParse(input.ovumSituationId, out int ovumSituationId))
//                    {
//                        course.OvumSituationId = ovumSituationId;
//                    }
//                    if (int.TryParse(input.ovumSourceId, out int ovumSourceId))
//                    {
//                        course.OvumSourceId = ovumSourceId;
//                    }
//                    if (int.TryParse(input.ovumOperationId, out int ovumOperationId))
//                    {
//                        course.OvumOperationId = ovumOperationId;
//                    }
//                    if (int.TryParse(input.spermSituationId, out int spermSituationId))
//                    {
//                        course.SpermSituationId = spermSituationId;
//                    }
//                    if (int.TryParse(input.spermSourceId, out int spermSourceId))
//                    {
//                        course.SpermSourceId = spermSourceId;
//                    }
//                    if (int.TryParse(input.spermOperationId, out int spermOperationId))
//                    {
//                        course.SpermOperationId = spermOperationId;
//                    }
//                    if (int.TryParse(input.SpermRetrievalMethodId, out int spermRetrievalMethodId))
//                    {
//                        course.SpermRetrievalMethodId = spermRetrievalMethodId;
//                    }
//                    if (int.TryParse(input.embryoSituationId, out int embryoSituationId))
//                    {
//                        course.EmbryoSituationId = embryoSituationId;
//                    }
//                    if (int.TryParse(input.embryoOperationId, out int embryoOperationId))
//                    {
//                        course.EmbryoOperationId = embryoOperationId;
//                    }
//                    dbContext.CourseOfTreatments.Add(course);
//                    dbContext.SaveChanges();
//                    scope.Complete();
//                }
//                result.SetSuccess();
//            }
//            catch(Exception ex)
//            {
//                result.SetError(ex.Message);
//            }
//            return result;
//        }
//        public async Task<BaseResponseDto> AddOvumFreeze(AddOvumFreezeDto input)
//        {
//            BaseResponseDto result = new BaseResponseDto();
//            try
//            {
//                AddOvumFreezeValidation(input);
//                using(TransactionScope scope = new TransactionScope())
//                {
//                    OvumFreeze ovumFreeze = new OvumFreeze
//                    {
//                        FreezeTime = input.freezeTime,
//                        Embryologist = input.embryologist,
//                        StorageUnitId = input.storageUnitId,
//                        MediumInUseId = input.mediumInUseId,
//                        OtherMediumName = input.otherMediumName,
//                        Memo = input.memo,
//                        OvumMorphologyA = input.ovumMorphology_A,
//                        OvumMorphologyB = input.ovumMorphology_B,
//                        OvumMorphologyC = input.ovumMorphology_C,
//                        TopColorId = input.topColorId,
//                        IsThawed = false
//                    };
//                    dbContext.OvumFreezes.Add(ovumFreeze);
//                    dbContext.SaveChanges();
//                    Guid latestOvumFreezeId = dbContext.OvumFreezes.OrderByDescending(x=>x.SqlId).Select(x=>x.OvumFreezeId).FirstOrDefault();
//                    var ovumDetails = dbContext.OvumDetails.Where(x => input.ovumDetailId.Contains(x.OvumDetailId));
//                    foreach(var i in ovumDetails)
//                    {
//                        i.OvumDetailStatusId = (int)OvumDetailStatusEnum.Freeze;
//                        i.OvumFreezeId = latestOvumFreezeId;
//                    }
//                    dbContext.SaveChanges();
//                    var storageUnit = dbContext.StorageUnits.FirstOrDefault(x => x.SqlId == input.storageUnitId);
//                    if (storageUnit != null)
//                    {
//                        storageUnit.IsOccupied = true;
//                    }
//                    else
//                    {
//                        throw new Exception("儲位資訊有誤");
//                    }
//                    dbContext.SaveChanges();
//                    scope.Complete();
//                }
//                result.SetSuccess();
//            }
//            catch(Exception ex)
//            {
//                result.SetError(ex.Message);
//            }
//            return result;
//        }
//        private void AddOvumFreezeValidation(AddOvumFreezeDto input)
//        {
//            if (input.ovumDetailId.Count <= 0 || input.ovumDetailId.Count > 4)
//            {
//                throw new Exception("卵子數量請介於 1-4");
//            }
//            var isFreezed = dbContext.OvumDetails.FirstOrDefault(x => input.ovumDetailId.Contains(x.OvumDetailId) && x.OvumDetailStatusId == (int)OvumDetailStatusEnum.Freeze);
//            if (isFreezed != null)
//            {
//                throw new Exception($"卵子編號: {isFreezed.OvumNumber} 已冷凍入庫");
//            }
//            var hasFreezeObservationNote = dbContext.OvumDetails.Where(x => input.ovumDetailId.Contains(x.OvumDetailId)).Include(x => x.ObservationNotes).FirstOrDefault(x => !x.ObservationNotes.Any(y => y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation));
//            if (hasFreezeObservationNote != null)
//            {
//                throw new Exception($"卵子編號: {hasFreezeObservationNote.OvumNumber} 尚無冷凍觀察紀錄");
//            }
//        }
//        public async Task<BaseResponseDto> UpdateOvumFreeze(AddOvumFreezeDto input)
//        {
//            BaseResponseDto result = new BaseResponseDto();
//            try
//            {
//                using(TransactionScope scope = new TransactionScope())
//                {
//                    if (input.ovumDetailId.Count <= 0)
//                    {
//                        throw new Exception("請選擇要修改的卵子");
//                    }
//                    var ovumFreeze = dbContext.OvumDetails.Where(x => x.OvumDetailId == input.ovumDetailId[0]).Select(x => x.OvumFreeze).FirstOrDefault();
//                    if (ovumFreeze == null)
//                    {
//                        throw new Exception("無相關的卵子資訊");
//                    }
//                    ovumFreeze.FreezeTime = input.freezeTime;
//                    ovumFreeze.Embryologist = input.embryologist;
//                    ovumFreeze.OvumMorphologyA = input.ovumMorphology_A;
//                    ovumFreeze.OvumMorphologyB = input.ovumMorphology_B;
//                    ovumFreeze.OvumMorphologyC = input.ovumMorphology_C;
//                    ovumFreeze.MediumInUseId = input.mediumInUseId;
//                    ovumFreeze.OtherMediumName = input.otherMediumName;
//                    ovumFreeze.Memo = input.memo;
//                    dbContext.SaveChanges();
//                    scope.Complete();
//                }
//                result.SetSuccess();
//            }
//            catch(Exception ex)
//            {
//                result.SetError(ex.Message);
//            }
            
//            return result; 
//        }
//        public async Task<AddOvumFreezeDto> GetOvumFreeze(Guid ovumDetailId)
//        {
//            var ovumFreeze = await dbContext.OvumDetails.Where(x => x.OvumDetailId == ovumDetailId).Select(x => new AddOvumFreezeDto
//            {
//                freezeTime = x.OvumFreeze == null ? default : x.OvumFreeze.FreezeTime,
//                embryologist = x.OvumFreeze == null ? default : x.OvumFreeze.Embryologist,
//                mediumInUseId = x.OvumFreeze == null ? default : x.OvumFreeze.MediumInUseId,
//                otherMediumName = x.OvumFreeze == null ? default : x.OvumFreeze.OtherMediumName,
//                ovumMorphology_A = x.OvumFreeze == null ? default : x.OvumFreeze.OvumMorphologyA,
//                ovumMorphology_B = x.OvumFreeze == null ? default : x.OvumFreeze.OvumMorphologyB,
//                ovumMorphology_C = x.OvumFreeze == null ? default : x.OvumFreeze.OvumMorphologyC,
//                memo = x.OvumFreeze == null ? default : x.OvumFreeze.Memo
//            }).FirstOrDefaultAsync();
//            return ovumFreeze;
//        }
//        public async Task<BaseCustomerInfoDto> GetOvumOwnerInfo(Guid ovumDetailId)
//        {
//            var result = await dbContext.OvumDetails.Where(x => x.OvumDetailId == ovumDetailId).Select(x => new BaseCustomerInfoDto
//            {
//                birthday = x.CourseOfTreatment.Customer.Birthday,
//                customerName = x.CourseOfTreatment.Customer.Name,
//                customerSqlId = x.CourseOfTreatment.Customer.SqlId
//            }).FirstOrDefaultAsync();
//            if (result == null)
//            {
//                return new BaseCustomerInfoDto();
//            }
//            return result;
//        }
//        public async Task<List<CommonDto>> GetTopColors()
//        {
//            return await dbContext.TopColors.Select(x=>new CommonDto
//            {
//                id = x.SqlId,
//                name = x.Name
//            }).ToListAsync();
//        }
//        public async Task<List<CommonDto>> GetFertilizationMethods()
//        {
//            return await dbContext.FertilizationMethods.Select(x=>new CommonDto
//            {
//                id = x.SqlId,
//                name = x.Name
//            }).OrderBy(x=>x.id).ToListAsync();
//        }
//        public async Task<List<CommonDto>> GetIncubators()
//        {
//            return await dbContext.Incubators.Select(x => new CommonDto
//            {
//                id = x.SqlId,
//                name = x.Name
//            }).OrderBy(x => x.id).ToListAsync();
//        }
//        public BaseResponseDto AddFertilization(AddFertilizationDto input)
//        {
//            BaseResponseDto result = new BaseResponseDto();
//            try
//            {
//                using(TransactionScope scope = new TransactionScope())
//                {
//                    Fertilization fertilization = new Fertilization
//                    {
//                        FertilizationTime = input.fertilizationTime,
//                        Embryologist = input.embryologist,
//                        FertilizationMethodId = input.fertilizationMethodId,
//                        IncubatorId = input.incubatorId,
//                        OtherIncubator = input.otherIncubator
//                    };
//                    sharedFunction.SetMediumInUse(fertilization, input.mediumInUseIds);
//                    dbContext.Fertilizations.Add(fertilization);
//                    dbContext.SaveChanges();
//                    Guid latestFertilisationId = dbContext.Fertilizations.OrderByDescending(x=>x.SqlId).Select(x=>x.FertilizationId).FirstOrDefault();
//                    if (latestFertilisationId == Guid.Empty)
//                    {
//                        throw new Exception("Fertilisation 資料表寫入也誤");
//                    }
//                    var ovumDetailIds = dbContext.OvumDetails.Where(x=>input.ovumDetailIds.Contains(x.OvumDetailId)).ToList();
//                    foreach (var i in ovumDetailIds)
//                    {
//                        i.FertilizationId = latestFertilisationId;
//                    }
//                    dbContext.SaveChanges();
//                    scope.Complete();
//                }
//                result.SetSuccess();
//            }
//            catch(Exception ex)
//            {
//                result.SetError(ex.Message);
//            }
//            return result;
//        }

//        public BaseResponseDto AddOvumThaw(AddOvumThawDto input)
//        {
//            BaseResponseDto result = new BaseResponseDto();
//            try
//            {
//                AddOvumThawValidation(input);
//                using(TransactionScope scope = new TransactionScope())
//                {
//                    #region 增加一筆 OvumThaw 的資訊，並得到此筆 OvumThaw 的 OvumThawId
//                    OvumThaw ovumThaw = new OvumThaw
//                    {
//                        ThawTime = input.thawTime,
//                        ThawMediumInUseId = input.thawMediumInUseId,
//                        Embryologist = input.embryologist,
//                        RecheckEmbryologist = input.recheckEmbryologist
//                    };
//                    sharedFunction.SetMediumInUse<OvumThaw>(ovumThaw, input.mediumInUseIds);
//                    dbContext.OvumThaws.Add(ovumThaw);
//                    dbContext.SaveChanges();
//                    Guid latestOvumThawId = dbContext.OvumThaws.OrderByDescending(x=>x.SqlId).Select(x=>x.OvumThawId).FirstOrDefault();
//                    #endregion
                    
//                    var freezeOvumDetails = dbContext.OvumDetails.Where(x => x.OvumFreezeId != null && input.freezeOvumDetailIds.Contains(x.OvumDetailId)).Select(x => new
//                    {
//                        ovumFreeze = x.OvumFreeze,
//                        storageUnit = x.OvumFreeze.StorageUnit,
//                        ovumDetail = x,
//                        observationNoteCount = x.ObservationNotes.Count,
//                        isTransferred = x.OvumTransferPairRecipientOvumDetails.Count > 0 ? true : false
//                    });
//                    foreach (var i in freezeOvumDetails)
//                    {
//                        i.ovumFreeze.IsThawed = true;
//                        i.storageUnit.IsOccupied = false;
//                        if (i.observationNoteCount == 0 && i.isTransferred)
//                        {
//                            i.ovumDetail.OvumFreezeId = null;
//                            i.ovumDetail.OvumDetailStatusId = (int)OvumDetailStatusEnum.Incubation;
//                            i.ovumDetail.OvumThawId = latestOvumThawId;
//                            OvumThawFreezePair pair = new OvumThawFreezePair
//                            {
//                                FreezeOvumDetailId = i.ovumDetail.OvumDetailId,
//                                ThawOvumDetailId = i.ovumDetail.OvumDetailId
//                            };
//                            dbContext.OvumThawFreezePairs.Add(pair);
//                        }
//                    }
//                    dbContext.SaveChanges();
//                    int count = 0;
//                    foreach (var i in freezeOvumDetails.ToList())
//                    {
//                        if (i.observationNoteCount == 0 && i.isTransferred){}
//                        else
//                        {
//                            OvumDetail ovumDetail = new OvumDetail
//                            {
//                                CourseOfTreatmentId = input.courseOfTreatmentId,
//                                OvumNumber = count + 1,
//                                OvumDetailStatusId = (int)OvumDetailStatusEnum.Incubation,
//                                OvumThawId = latestOvumThawId,
//                                FertilizationId = i.ovumDetail.FertilizationId,
//                                OvumFromCourseOfTreatmentId = i.ovumDetail.OvumFromCourseOfTreatmentId
//                            };
//                            dbContext.OvumDetails.Add(ovumDetail);
//                            dbContext.SaveChanges();
//                            Guid thawOvumDetailId = dbContext.OvumDetails.OrderByDescending(x => x.SqlId).Select(x => x.OvumDetailId).FirstOrDefault();
//                            OvumThawFreezePair pair = new OvumThawFreezePair
//                            {
//                                FreezeOvumDetailId = i.ovumDetail.OvumDetailId,
//                                ThawOvumDetailId = thawOvumDetailId
//                            };
//                            dbContext.OvumThawFreezePairs.Add(pair);
//                            dbContext.SaveChanges();
//                        }
//                        count++;
//                    }
//                    dbContext.SaveChanges();
//                    scope.Complete();
//                }
//                result.SetSuccess();
//            }
//            catch(Exception ex)
//            {
//                result.SetError(ex.Message);
//            }
//            return result;
//        }
//        private void AddOvumThawValidation(AddOvumThawDto input)
//        {
//            if (input.freezeOvumDetailIds == null || input.freezeOvumDetailIds.Count <= 0)
//            {
//                throw new Exception("請選擇要解凍的卵子");
//            }
//            if (input.mediumInUseIds == null || input.mediumInUseIds.Count <= 0)
//            {
//                throw new Exception("請選擇培養液");
//            }
//            var hasThawedOvumDetails = dbContext.OvumDetails.Where(x => input.freezeOvumDetailIds.Contains(x.OvumDetailId) && x.OvumThawFreezePairFreezeOvumDetails.Any()).GroupBy(x => x.CourseOfTreatment.SqlId).Select(y => new
//            {
//                courseOfTreatmentSqlId = y.Key,
//                ovumNumbers = y.Select(z => z.OvumNumber).OrderBy(z=>z).ToList()
//            });
//            if (hasThawedOvumDetails.Any())
//            {
//                string errorMessage = "";
//                foreach(var i in hasThawedOvumDetails)
//                {
//                    string message = $"療程編號: {i.courseOfTreatmentSqlId} ，卵子編號: ";
//                    foreach(var j in i.ovumNumbers)
//                    {
//                        message += $"{j}, ";
//                    }
//                    message = message.Substring(0, message.Length - 2);
//                    message += "已解凍\n";
//                    errorMessage += message;
//                }
//                throw new Exception(errorMessage);
//            }
//        }

//        public async Task<BaseResponseDto> OvumBankTransfer(OvumBankTransferDto input)
//        {
//            BaseResponseDto result = new BaseResponseDto();
//            try
//            {
//                if (input.transferOvumDetailIds == null || input.transferOvumDetailIds.Count < 1)
//                {
//                    throw new Exception("請選擇要轉移的卵子");
//                }
//                using(TransactionScope scope = new TransactionScope())
//                {
//                    // 將受贈者的OvumFromCourseOfTreatmentId 改成捐贈者的 CourseOfTreatmentId
//                    var recipientCourseOfTreatmentId = dbContext.CourseOfTreatments.Where(x => x.SqlId == input.recipientCourseOfTreatmentSqlId).Select(x=>x.CourseOfTreatmentId).FirstOrDefault();
//                    if (recipientCourseOfTreatmentId == Guid.Empty)
//                    {
//                        throw new Exception("查無此受贈者之療程編號");
//                    }

//                    var donorOvumDetails = dbContext.OvumDetails.Where(x => input.transferOvumDetailIds.Contains(x.OvumDetailId)).ToList();
//                    if (donorOvumDetails.Count != input.transferOvumDetailIds.Count)
//                    {
//                        throw new Exception("捐贈卵子資訊有誤");
//                    }
//                    for (int i = 0; i < donorOvumDetails.Count; i++)
//                    {
//                        // 加入新的 OvumDetail 資料
//                        OvumDetail ovumDetail = new OvumDetail
//                        {
//                            OvumNumber = i + 1,
//                            OvumFreezeId = donorOvumDetails[i].OvumFreezeId,
//                            FertilizationId = donorOvumDetails[i].FertilizationId,
//                            CourseOfTreatmentId = recipientCourseOfTreatmentId,
//                            OvumFromCourseOfTreatmentId = donorOvumDetails[i].OvumFromCourseOfTreatmentId
//                        };
//                        ovumDetail.OvumDetailStatusId = donorOvumDetails[i].OvumFreezeId == null ? (int)OvumDetailStatusEnum.Incubation : (int)OvumDetailStatusEnum.Freeze;
//                        dbContext.OvumDetails.Add(ovumDetail);
//                        dbContext.SaveChanges();
//                        Guid latestOvumDetailId = dbContext.OvumDetails.OrderByDescending(x => x.SqlId).Select(x=>x.OvumDetailId).FirstOrDefault();
//                        // 加入新的 OvumTransferPair 資料
//                        OvumTransferPair pair = new OvumTransferPair
//                        {
//                            RecipientOvumDetailId = latestOvumDetailId,
//                            DonorOvumDetailId = donorOvumDetails[i].OvumDetailId
//                        };
//                        dbContext.OvumTransferPairs.Add(pair);
//                    }
//                    dbContext.SaveChanges();
//                    scope.Complete();
//                }
//                result.SetSuccess();
//            }
//            catch (Exception ex)
//            {
//                result.SetError(ex.Message);
//            }
//            return result;
//        }
        
//    }
//}
