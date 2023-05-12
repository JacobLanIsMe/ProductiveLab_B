using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForOperateSperm;
using prjProductiveLab_B.Enums;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;
using System.Transactions;

namespace prjProductiveLab_B.Services
{
    public class OperateSpermService : IOperateSpermService
    {
        private readonly ReproductiveLabContext dbContext;
        private readonly ISharedFunctionService sharedFunction;
        public OperateSpermService(ReproductiveLabContext dbContext, ISharedFunctionService sharedFunction)
        {
            this.dbContext = dbContext;
            this.sharedFunction = sharedFunction;
        }
        //public async Task<BaseOperateSpermInfoDto> GetOriginInfoOfSperm(Guid courseOfTreatmentId)
        //{
        //    var result = await dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new BaseOperateSpermInfoDto
        //    {
        //        spermSituationName = x.Treatment.SpermSituation.Name,
        //        spermRetrievalMethodName = x.SpermFromCourseOfTreatment.Treatment.SpermRetrievalMethod.Name,
        //        spermOwner = new BaseCustomerInfoDto
        //        {
        //            customerName = (x.Treatment.SpermSituationId == (int)GermCellSituationEnum.thaw || x.Treatment.SpermOperationId == (int)GermCellOperationEnum.freeze) ? x.SpermFromCourseOfTreatment.Customer.Name : x.Customer.SpouseNavigation.Name,
        //            customerSqlId = (x.Treatment.SpermSituationId == (int)GermCellSituationEnum.thaw || x.Treatment.SpermOperationId == (int)GermCellOperationEnum.freeze) ? x.SpermFromCourseOfTreatment.Customer.SqlId : x.Customer.SpouseNavigation.SqlId,
        //            birthday = (x.Treatment.SpermSituationId == (int)GermCellSituationEnum.thaw || x.Treatment.SpermOperationId == (int)GermCellOperationEnum.freeze) ? x.SpermFromCourseOfTreatment.Customer.Birthday : x.Customer.SpouseNavigation.Birthday
        //        }
        //    }).FirstOrDefaultAsync();
        //    if (result == null)
        //    {
        //        return new BaseOperateSpermInfoDto();
        //    }
        //    else
        //    {
        //        return result;
        //    }
        //}

        public async Task<List<SpermFreezeDto>> GetSpermFreeze(int customerSqlId)
        {

            return await dbContext.SpermFreezes.Where(x => x.CourseOfTreatment.Customer.SqlId == customerSqlId && x.IsThawed == false).Select(x => new SpermFreezeDto
            {
                spermFreezeId = x.SpermFreezeId,
                vialNumber = x.VialNumber,
                storageUnitName = x.StorageUnit.UnitName,
                storageStripBoxId = x.StorageUnit.StorageStripBoxId,
                storageCanistName = x.StorageUnit.StorageStripBox.StorageCanist.CanistName,
                storageTankName = x.StorageUnit.StorageStripBox.StorageCanist.StorageTank.TankName,
                storageUnitId = x.StorageUnitId,
                freezeTime = x.SpermFreezeSituation.FreezeTime
            }).OrderBy(x=>x.freezeTime).ThenBy(x => x.vialNumber).ToListAsync();
        }
        public async Task<List<SpermScoreDto>> GetSpermScores(Guid courseOfTreatmentId)
        {
            var courseOfTreatment = dbContext.CourseOfTreatments.FirstOrDefault(x=>x.CourseOfTreatmentId == courseOfTreatmentId);
            if (courseOfTreatment == null)
            {
                return new List<SpermScoreDto>();
            }
            var q = await dbContext.SpermScores.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new
            {
                isThawed = x.CourseOfTreatment.SpermThaws.Any() ? true : false,
                baseSpermInfo_Thaw = new BaseOperateSpermInfoDto
                {
                    spermSituationName = x.CourseOfTreatment.Treatment.SpermSituation == null ? null : x.CourseOfTreatment.Treatment.SpermSituation.Name,
                    //spermRetrievalMethodName = x.CourseOfTreatment.SpermThaws.Any() ? x.CourseOfTreatment
                    spermOwner = new BaseCustomerInfoDto
                    {
                        customerName = x.CourseOfTreatment.SpermThaws.Any() ? x.CourseOfTreatment.SpermThaws.First().SpermThawFreezePairs.Select(y=>y.SpermFreeze.CourseOfTreatment.Customer.Name).FirstOrDefault() : null,
                        customerSqlId = x.CourseOfTreatment.SpermThaws.Any() ? x.CourseOfTreatment.SpermThaws.First().SpermThawFreezePairs.Select(y=>y.SpermFreeze.CourseOfTreatment.Customer.SqlId).FirstOrDefault() : default,
                    }
                },
                baseSpermInfo_Fresh = new BaseOperateSpermInfoDto
                {
                    spermSituationName = x.CourseOfTreatment.Treatment.SpermSituation == null ? null : x.CourseOfTreatment.Treatment.SpermSituation.Name,
                    //spermRetrievalMethodName = x.CourseOfTreatment.SpermThaws.Any() ? x.CourseOfTreatment
                    spermOwner = new BaseCustomerInfoDto
                    {
                        customerName = x.CourseOfTreatment.Customer.GenderId == (int)GenderEnum.female && x.CourseOfTreatment.Customer.SpouseNavigation != null ? x.CourseOfTreatment.Customer.SpouseNavigation.Name : x.CourseOfTreatment.Customer.Name,
                        customerSqlId = x.CourseOfTreatment.Customer.GenderId == (int)GenderEnum.female && x.CourseOfTreatment.Customer.SpouseNavigation != null ? x.CourseOfTreatment.Customer.SpouseNavigation.SqlId : x.CourseOfTreatment.Customer.SqlId
                    }
                },
                volume = x.Volume,
                concentration = x.Concentration,
                activityA = x.ActivityA,
                activityB = x.ActivityB,
                activityC = x.ActivityC,
                activityD = x.ActivityD,
                morphology = x.Morphology,
                abstinence = x.Abstinence,
                spermScoreTimePointId = x.SpermScoreTimePointId,
                spermScoreTimePoint = x.SpermScoreTimePoint.TimePoint,
                recordTime = x.RecordTime,
                embryologist = x.Embryologist,
                embryologistName = x.EmbryologistNavigation.Name,
                courseOfTreatmentId = x.CourseOfTreatmentId,
                courseOfTreatmentSqlId = x.CourseOfTreatment.SqlId
            }).OrderBy(x => x.spermScoreTimePointId).ThenBy(x=>x.recordTime).ToListAsync();
            List<SpermScoreDto> result = new List<SpermScoreDto>();
            foreach (var i in q)
            {
                SpermScoreDto spermScore = new SpermScoreDto
                {
                    volume = i.volume,
                    concentration = i.concentration,
                    activityA = i.activityA,
                    activityB = i.activityB,
                    activityC = i.activityC,
                    activityD = i.activityD,
                    morphology = i.morphology,
                    abstinence = i.abstinence,
                    spermScoreTimePointId = i.spermScoreTimePointId,
                    spermScoreTimePoint = i.spermScoreTimePoint,
                    recordTime = i.recordTime,
                    embryologist = i.embryologist,
                    embryologistName = i.embryologistName,
                    courseOfTreatmentId = i.courseOfTreatmentId,
                    courseOfTreatmentSqlId = i.courseOfTreatmentSqlId
                };
                spermScore.baseSpermInfo = i.isThawed ? i.baseSpermInfo_Thaw : i.baseSpermInfo_Fresh;
                result.Add(spermScore);
            }
            return result;
        }
        public BaseResponseDto AddSpermScore(SpermScoreDto addSpermScore)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SpermScore spermScore = new SpermScore();
                    spermScore = TransformSpermScoreDtoToSpermScore(addSpermScore, spermScore);
                    dbContext.SpermScores.Add(spermScore);
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
       
        public async Task<BaseResponseDto> UpdateExistingSpermScore(SpermScoreDto addSpermScore)
        {
            BaseResponseDto result = new BaseResponseDto();
            var existingSpermScore = await dbContext.SpermScores.Where(x => x.CourseOfTreatmentId == addSpermScore.courseOfTreatmentId && x.SpermScoreTimePointId == addSpermScore.spermScoreTimePointId).FirstOrDefaultAsync();
            if (existingSpermScore != null) 
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        existingSpermScore = TransformSpermScoreDtoToSpermScore(addSpermScore, existingSpermScore);
                        dbContext.SaveChanges();
                        scope.Complete();
                    }
                    result.SetSuccess();
                }
                catch(Exception ex)
                {
                    result.SetError(ex.Message);
                }
            }
            else
            {
                result.SetError("找無此紀錄");
            }
            return result;
        }

        private SpermScore TransformSpermScoreDtoToSpermScore(SpermScoreDto spermScoreDto, SpermScore spermScore)
        {

            spermScore.Volume = spermScoreDto.volume;
            spermScore.Concentration = spermScoreDto.concentration;
            spermScore.ActivityA = spermScoreDto.activityA;
            spermScore.ActivityB = spermScoreDto.activityB;
            spermScore.ActivityC = spermScoreDto.activityC;
            spermScore.ActivityD = spermScoreDto.activityD;
            spermScore.Morphology = spermScoreDto.morphology;
            spermScore.Abstinence = spermScoreDto.abstinence;
            spermScore.SpermScoreTimePointId = spermScoreDto.spermScoreTimePointId;
            spermScore.RecordTime = spermScoreDto.recordTime;
            spermScore.Embryologist = spermScoreDto.embryologist;
            spermScore.CourseOfTreatmentId = spermScoreDto.courseOfTreatmentId;
            return spermScore;
        }

        public async Task<List<CommonDto>> GetSpermFreezeOperationMethod()
        {
            var result = await dbContext.SpermFreezeOperationMethods.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
            return result;
        }
        
        public async Task<BaseResponseDto> AddSpermFreeze(AddSpermFreezeDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            string errorMessage = AddSpermFreezeValidation(input);
            if (string.IsNullOrEmpty(errorMessage))
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        SpermFreezeSituation situation = new SpermFreezeSituation
                        {
                            Embryologist = input.embryologist,
                            FreezeTime = input.freezeTime,
                            SpermFreezeOperationMethodId = input.spermFreezeOperationMethodId,
                            FreezeMediumInUseId = input.freezeMedium,
                            OtherFreezeMediumName = input.otherFreezeMediumName
                        };
                        sharedFunction.SetMediumInUse<SpermFreezeSituation>(situation, input.mediumInUseArray);
                        dbContext.SpermFreezeSituations.Add(situation);
                        dbContext.SaveChanges();
                        Guid lastSituationId = dbContext.SpermFreezeSituations.OrderByDescending(x => x.SqlId).Select(x => x.SpermFreezeSituationId).FirstOrDefault();
                        if (lastSituationId == Guid.Empty)
                        {
                            throw new Exception("冷凍精子狀況有誤");
                        }
                        for (int i = 0; i < input.storageUnitId.Count; i++)
                        {
                            SpermFreeze spermFreeze = new SpermFreeze()
                            {
                                VialNumber = i + 1,
                                CourseOfTreatmentId = input.courseOfTreatmentId,
                                StorageUnitId = input.storageUnitId[i],
                                IsThawed = false,
                                SpermFreezeSituationId = lastSituationId
                            };
                            dbContext.SpermFreezes.Add(spermFreeze);
                            var storageUnit = dbContext.StorageUnits.FirstOrDefault(x => x.SqlId == input.storageUnitId[i]);
                            if (storageUnit != null && storageUnit.IsOccupied == false)
                            {
                                storageUnit.IsOccupied = true;
                            }
                            else
                            {
                                throw new Exception("儲位資訊有誤");
                            }
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
            }
            else
            {
                result.SetError(errorMessage);
            }
            
            return result;
        }

        
        private string AddSpermFreezeValidation(AddSpermFreezeDto input)
        {
            string errorMessage = "";
            if (input.mediumInUseArray == null || input.mediumInUseArray.Count == 0 || input.mediumInUseArray.Count > 3)
            {
                errorMessage += "培養液資訊有誤\n";
            }
            if(input.storageUnitId == null || input.storageUnitId.Count == 0) 
            {
                errorMessage += "請選擇儲位\n";
            }
            return errorMessage;
        }

        public async Task<List<CommonDto>> GetSpermThawMethods()
        {
            return await dbContext.SpermThawMethods.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).ToListAsync();
        }

        public BaseResponseDto AddSpermThaw(AddSpermThawDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using(TransactionScope scope = new TransactionScope())
                {
                    SpermThaw spermThaw = new SpermThaw
                    {
                        CourseOfTreatmentId = input.courseOfTreatmentId,
                        SpermThawMethodId = input.spermThawMethodId,
                        ThawTime = input.thawTime,
                        Embryologist = input.embryologist,
                        RecheckEmbryologist = input.recheckEmbryologist,
                        OtherSpermThawMethod = input.otherSpermThawMethod,
                    };
                    sharedFunction.SetMediumInUse(spermThaw, input.mediumInUseIds);
                    dbContext.SpermThaws.Add(spermThaw);
                    dbContext.SaveChanges();
                    Guid latestSpermThawId = dbContext.SpermThaws.OrderByDescending(x=>x.SqlId).Select(x=>x.SpermThawId).FirstOrDefault();
                    if (latestSpermThawId == Guid.Empty)
                    {
                        throw new Exception("spermThaw 資料表寫入錯誤");
                    }
                    foreach (var i in input.spermFreezeIds)
                    {
                        SpermThawFreezePair pair = new SpermThawFreezePair
                        {
                            SpermThawId = latestSpermThawId,
                            SpermFreezeId = i
                        };
                        dbContext.SpermThawFreezePairs.Add(pair);
                        var spermFreeze = dbContext.SpermFreezes.Where(x => x.SpermFreezeId == i).Select(x => new
                        {
                            spermFreeze = x,
                            storageUnit = x.StorageUnit
                        }).FirstOrDefault();
                        if (spermFreeze == null)
                        {
                            throw new Exception("SpermThawFreezePair資料表寫入錯誤");
                        }
                        spermFreeze.spermFreeze.IsThawed = true;
                        spermFreeze.storageUnit.IsOccupied = false;
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
    }
}
