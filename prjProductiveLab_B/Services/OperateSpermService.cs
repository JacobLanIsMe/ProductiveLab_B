using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Models;
using System.Linq;
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
            BaseOperateSpermInfo? baseOperateSpermInfo = await dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new BaseOperateSpermInfo
            {
                spermRetrievalMethod = x.SpermRetrievalMethod.Name,
                spermFreezes = dbContext.SpermFreezes.Where(y => y.CourseOfTreatmentId == x.SpermFromCourseOfTreatmentId && y.IsThawed == false).Select(y => new SpermFreezeDto
                {
                    spermFreezeId = y.SpermFreezeId,
                    vialNumber = y.VialNumber,
                    storageUnitName = y.StorageUnit.UnitName,
                    storageCaneBoxName = y.StorageUnit.StorageCaneBox.CaneBoxName,
                    storageShelfName = y.StorageUnit.StorageCaneBox.StorageShelf.ShelfName,
                    storageTankName = y.StorageUnit.StorageCaneBox.StorageShelf.StorageTank.TankName,
                    storageUnitId = y.StorageUnitId
                }).OrderBy(y=>y.vialNumber).ToList(),
                treatmentId = x.TreatmentId,
                spermFromCourseOfTreatmentId = x.SpermFromCourseOfTreatmentId,
                treatmentIdOfSpermFromCourseOfTreatmentId = dbContext.CourseOfTreatments.Where(y => y.CourseOfTreatmentId == x.SpermFromCourseOfTreatmentId).Select(y => y.TreatmentId).FirstOrDefault()
            }).AsNoTracking().FirstOrDefaultAsync();

            if (baseOperateSpermInfo != null)
            {
                if (baseOperateSpermInfo.spermFromCourseOfTreatmentId == courseOfTreatmentId)
                {
                    baseOperateSpermInfo.isFresh = true;
                }
                else
                {
                    baseOperateSpermInfo.isFresh = false;
                }
                if (baseOperateSpermInfo.treatmentId == 20 || baseOperateSpermInfo.treatmentId == 21 || baseOperateSpermInfo.treatmentId == 22)
                {
                    baseOperateSpermInfo.husband = dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new BaseCustomerInfoDto
                    {
                        customerSqlId = x.Customer.SqlId,
                        customerName = x.Customer.Name,
                        birthday = x.Customer.Birthday
                    }).FirstOrDefault();
                }
                else
                {
                    baseOperateSpermInfo.husband = dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new BaseCustomerInfoDto
                    {
                        customerSqlId = x.Customer.SpouseNavigation.SqlId,
                        customerName = x.Customer.SpouseNavigation.Name,
                        birthday = x.Customer.SpouseNavigation.Birthday
                    }).FirstOrDefault();
                }
                if (baseOperateSpermInfo.treatmentIdOfSpermFromCourseOfTreatmentId == 20 || baseOperateSpermInfo.treatmentIdOfSpermFromCourseOfTreatmentId == 21 || baseOperateSpermInfo.treatmentIdOfSpermFromCourseOfTreatmentId == 22)
                {
                    baseOperateSpermInfo.spermOwner = dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == baseOperateSpermInfo.spermFromCourseOfTreatmentId).Select(x => new BaseCustomerInfoDto
                    {
                        customerSqlId = x.Customer.SqlId,
                        customerName = x.Customer.Name,
                        birthday = x.Customer.Birthday
                    }).FirstOrDefault();
                }
                else
                {
                    baseOperateSpermInfo.spermOwner = dbContext.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == baseOperateSpermInfo.spermFromCourseOfTreatmentId).Select(x => new BaseCustomerInfoDto
                    {
                        customerSqlId = x.Customer.SpouseNavigation.SqlId,
                        customerName = x.Customer.SpouseNavigation.Name,
                        birthday = x.Customer.SpouseNavigation.Birthday
                    }).FirstOrDefault();
                }
            }
            BaseOperateSpermInfoDto result = new BaseOperateSpermInfoDto()
            {
                husband = baseOperateSpermInfo.husband,
                isFresh = baseOperateSpermInfo.isFresh,
                spermRetrievalMethod = baseOperateSpermInfo.spermRetrievalMethod,
                spermOwner = baseOperateSpermInfo.spermOwner,
                spermFreezes = baseOperateSpermInfo.spermFreezes,
                spermFromCourseOfTreatmentId = baseOperateSpermInfo.spermFromCourseOfTreatmentId
            };
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
        public async Task<SpermScoreDto> GetExistingSpermScore(Guid spermFromCourseOfTreatmentId, int spermScoreTimePointId)
        {
            var existingSpermScore = await dbContext.SpermScores.Where(x => x.CourseOfTreatmentId == spermFromCourseOfTreatmentId && x.SpermScoreTimePointId == spermScoreTimePointId).Select(x => new SpermScoreDto
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
                recordTime = x.RecordTime,
                embryologist = x.Embryologist,
                courseOfTreatmentId = x.CourseOfTreatmentId
            }).OrderByDescending(x => x.recordTime).AsNoTracking().FirstOrDefaultAsync();
            return existingSpermScore;
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

        public async Task<List<SpermFreezeOperateMethodDto>> GetSpermFreezeOperationMethod()
        {
            var result = await dbContext.SpermFreezeOperationMethods.Select(x => new SpermFreezeOperateMethodDto
            {
                spermFreezeOperateMethodSqlId = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.spermFreezeOperateMethodSqlId).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<BaseResponseDto> AddSpermFreeze(AddSpermFreezeDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            string errorMessage = AddSpermFreezeValidation(input);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                result.SetError(errorMessage);
                return result;
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    for (int i = 0; i < input.storageUnitId.Count; i++)
                    {
                        SpermFreeze spermFreeze = new SpermFreeze()
                        {
                            VialNumber = i + 1,
                            CourseOfTreatmentId = input.courseOfTreatmentId,
                            MediumInUseId1 = input.mediumInUseArray[0],
                            StorageUnitId = input.storageUnitId[i],
                            Embryologist = input.embryologist,
                            FreezeTime = input.freezeTime,
                            SpermFreezeOperationMethodId = input.spermFreezeOperationMethodId,
                            FreezeMediumInUseId = input.freezeMedium,
                            IsThawed = false
                        };
                        if (input.mediumInUseArray.Count > 1)
                        {
                            spermFreeze.MediumInUseId2 = input.mediumInUseArray[1];
                        }
                        if (input.mediumInUseArray.Count > 2)
                        {
                            spermFreeze.MediumInUseId3 = input.mediumInUseArray[2];
                        }
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
            catch(Exception ex)
            {
                result.SetError(ex.Message);
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
                        var unit = dbContext.StorageUnits.Where(x => x.SqlId == i).Include(x => x.SpermFreezes).FirstOrDefault();
                        if (unit == null || unit.IsOccupied == false)
                        {
                            throw new Exception("儲位資訊有誤");
                        }
                        else
                        {
                            unit.IsOccupied = false;
                            unit.SpermFreezes.FirstOrDefault().IsThawed = true;
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
    }
}
