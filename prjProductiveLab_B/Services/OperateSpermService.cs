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
        public OperateSpermService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BaseOperateSpermInfoDto> GetOriginInfoOfSperm(Guid courseOfTreatmentId)
        {
            var result = await dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new BaseOperateSpermInfoDto
            {
                spermSituationName = x.Treatment.SpermSituation.Name,
                spermRetrievalMethodName = x.SpermFromCourseOfTreatment.Treatment.SpermRetrievalMethod.Name,
                spermOwner = new BaseCustomerInfoDto
                {
                    customerName = (x.Treatment.SpermSituationId == (int)GermCellSituationEnum.thaw || x.Treatment.SpermOperationId == (int)GermCellOperationEnum.freeze) ? x.SpermFromCourseOfTreatment.Customer.Name : x.Customer.SpouseNavigation.Name,
                    customerSqlId = (x.Treatment.SpermSituationId == (int)GermCellSituationEnum.thaw || x.Treatment.SpermOperationId == (int)GermCellOperationEnum.freeze) ? x.SpermFromCourseOfTreatment.Customer.SqlId : x.Customer.SpouseNavigation.SqlId,
                    birthday = (x.Treatment.SpermSituationId == (int)GermCellSituationEnum.thaw || x.Treatment.SpermOperationId == (int)GermCellOperationEnum.freeze) ? x.SpermFromCourseOfTreatment.Customer.Birthday : x.Customer.SpouseNavigation.Birthday
                }
            }).FirstOrDefaultAsync();
            if (result == null)
            {
                return new BaseOperateSpermInfoDto();
            }
            else
            {
                return result;
            }
        }

        public async Task<List<SpermFreezeDto>> GetSpermFreeze(Guid spermFromCourseOfTreatmentId)
        {
            return await dbContext.SpermFreezes.Where(x => x.CourseOfTreatmentId == spermFromCourseOfTreatmentId && x.IsThawed == false).Select(x => new SpermFreezeDto
            {
                spermFreezeId = x.SpermFreezeId,
                vialNumber = x.VialNumber,
                storageUnitName = x.StorageUnit.UnitName,
                storageStripBoxId = x.StorageUnit.StorageStripBoxId,
                storageCanistName = x.StorageUnit.StorageStripBox.StorageCanist.CanistName,
                storageTankName = x.StorageUnit.StorageStripBox.StorageCanist.StorageTank.TankName,
                storageUnitId = x.StorageUnitId
            }).OrderBy(x => x.vialNumber).ToListAsync();
        }
        public async Task<List<SpermScoreDto>> GetSpermScore(Guid courseOfTreatmentId)
        {
            return await dbContext.SpermScores.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new SpermScoreDto
            {
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
                courseOfTreatmentId = x.CourseOfTreatmentId
            }).OrderBy(x => x.spermScoreTimePointId).ThenBy(x=>x.recordTime).ToListAsync();
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
                        if (input.mediumInUseArray.Count > 0 && input.mediumInUseArray[0] != null)
                        {
                            situation.MediumInUseId1 = (Guid)input.mediumInUseArray[0];
                        }
                        if (input.mediumInUseArray.Count > 1 && input.mediumInUseArray[1] != null)
                        {
                            situation.MediumInUseId2 = input.mediumInUseArray[1];
                        }
                        if (input.mediumInUseArray.Count > 2 && input.mediumInUseArray[2] != null)
                        {
                            situation.MediumInUseId3 = input.mediumInUseArray[2];
                        }
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

        public async Task<BaseResponseDto> SelectSpermFreeze(List<int> unitIds)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var i in unitIds)
                    {
                        var spermFreeze = dbContext.SpermFreezes.FirstOrDefault(x=>x.StorageUnitId == i);
                        if (spermFreeze == null)
                        {
                            throw new Exception("冷凍精蟲資訊有誤");
                        }
                        else
                        {
                            spermFreeze.IsThawed = true;
                        }
                        var storageUnit = dbContext.StorageUnits.FirstOrDefault(x => x.SqlId == i);
                        if (storageUnit == null || storageUnit.IsOccupied == false)
                        {
                            throw new Exception("儲位資訊有誤");
                        }
                        else
                        {
                            storageUnit.IsOccupied = false;
                        }
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

        private string AddSpermFreezeValidation(AddSpermFreezeDto input)
        {
            string errorMessage = "";
            if (input.mediumInUseArray == null || input.mediumInUseArray.Count == 0 || input.mediumInUseArray.Count > 3)
            {
                errorMessage += "培養液資訊有誤\n";
            }
            if(input.storageUnitId == null || input.storageUnitId.Count == 0) 
            {
                errorMessage += "儲位資訊有誤\n";
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
    }
}
