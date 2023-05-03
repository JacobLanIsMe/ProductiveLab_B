using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForStorage;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;
using System.Text;
using System.Transactions;

namespace prjProductiveLab_B.Services
{
    public class StorageService : IStorageService
    {
        private readonly ReproductiveLabContext dbContext;
        private readonly ITreatmentService treatmentService;
        public StorageService(ReproductiveLabContext dbContext, ITreatmentService treatmentService)
        {
            this.dbContext = dbContext;
            this.treatmentService = treatmentService;
        }

        public async Task<List<StorageTankStatusDto>> GetStorageTankStatus()
        {
            var result = await dbContext.StorageUnits.GroupBy(x => new { x.StorageStripBox.StorageCanist.StorageTankId, x.StorageStripBox.StorageCanistId }).Select(y => new StorageTankStatusDto
            {
                tankId = y.Key.StorageTankId,
                tankInfo = dbContext.StorageTanks.Where(z => z.SqlId == y.Key.StorageTankId).Select(z => new StorageTankDto
                {
                    tankName = z.TankName,
                    tankTypeId = z.StorageTankTypeId
                }).FirstOrDefault(),
                canistId = y.Key.StorageCanistId,
                canistName = dbContext.StorageCanists.Where(z => z.SqlId == y.Key.StorageCanistId).Select(z => z.CanistName).FirstOrDefault(),
                emptyAmount = y.Where(z => z.IsOccupied == false).Count(),
                occupiedAmount = y.Where(z => z.IsOccupied == true).Count(),
                totalAmount = y.Count(),
                unitInfos = y.GroupBy(z=>z.StorageStripBoxId).Select(a=>new StorageUnitStatusDto
                {
                    stripIdOrBoxId = a.Key,
                    stripNameOrBoxName = dbContext.StorageStripBoxes.Where(b=>b.SqlId == a.Key).Select(b=>b.StripBoxName).FirstOrDefault(),
                    stripBoxEmptyUnit = a.Where(b=>b.IsOccupied == false).Count(),
                    storageUnitInfo = a.Select(b=> new StorageUnitDto
                    {
                        storageUnitId = b.SqlId,
                        unitName = b.UnitName,
                        isOccupied = b.IsOccupied
                    }).OrderBy(b=>b.storageUnitId).ToList()
                }).OrderBy(a=>a.stripIdOrBoxId).ToList()
            }).OrderBy(x=>x.tankId).ThenBy(x=>x.canistId).ToListAsync();
            return result;
        }

        public async Task<List<StorageUnitStatusDto>> GetStorageUnitStatus(int tankId, int canistId)
        {
            var result = await dbContext.StorageUnits.Where(x => x.StorageStripBox.StorageCanist.StorageTankId == tankId && x.StorageStripBox.StorageCanistId == canistId).GroupBy(x => x.StorageStripBoxId).Select(y => new StorageUnitStatusDto
            {
                stripIdOrBoxId = y.Key,
                stripNameOrBoxName = dbContext.StorageStripBoxes.Where(z=>z.SqlId == y.Key).Select(z=>z.StripBoxName).FirstOrDefault(),
                stripBoxEmptyUnit = y.Where(z=>z.IsOccupied == false).Count(),
                storageUnitInfo = y.Select(z => new StorageUnitDto
                {
                    storageUnitId = z.SqlId,
                    unitName = z.UnitName,
                    isOccupied = z.IsOccupied,
                }).OrderBy(z => z.storageUnitId).ToList()
            }).OrderBy(y => y.stripIdOrBoxId).ToListAsync();
            return result;
        }

        public async Task<BaseResponseDto> AddStorageTank(StorageAddNewTankDto storageAddNewTankDto)
        {
            BaseResponseDto result = new BaseResponseDto();
            if (string.IsNullOrEmpty(storageAddNewTankDto.tankName))
            {
                result.SetError("液態氮桶的名稱未填");
                return result;
            }
            if (!string.IsNullOrEmpty(storageAddNewTankDto.tankName) && await HasTankName(storageAddNewTankDto.tankName))
            {
                result.SetError("液態氮桶的名稱已存在");
                return result;
            }
            
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    StorageTank storageTank = new StorageTank()
                    {
                        TankName = storageAddNewTankDto.tankName,
                        StorageTankTypeId = storageAddNewTankDto.tankTypeId
                    };
                    dbContext.StorageTanks.Add(storageTank);
                    dbContext.SaveChanges();
                    int latestStorageTankId = dbContext.StorageTanks.Select(x => x.SqlId).OrderByDescending(x => x).FirstOrDefault();
                    for (int i = 1; i <= storageAddNewTankDto.canistAmount; i++)
                    {
                        StorageCanist storageCanist = new StorageCanist()
                        {
                            CanistName = i.ToString(),
                            StorageTankId = latestStorageTankId
                        };
                        dbContext.StorageCanists.Add(storageCanist);
                        dbContext.SaveChanges();
                        int latestStorageCanistId = dbContext.StorageCanists.Select(y => y.SqlId).OrderByDescending(y => y).FirstOrDefault();
                        for (int j = 1; j <= storageAddNewTankDto.stripBoxAmount; j++)
                        {
                            StorageStripBox storageStripBox = new StorageStripBox()
                            {
                                StripBoxName = j.ToString(),
                                StorageCanistId = latestStorageCanistId
                            };
                            dbContext.StorageStripBoxes.Add(storageStripBox);
                            dbContext.SaveChanges();
                            int latestStorageStripBoxId = dbContext.StorageStripBoxes.Select(z => z.SqlId).OrderByDescending(z => z).FirstOrDefault();
                            for (int k = 1; k <= storageAddNewTankDto.unitAmount; k++)
                            {
                                StorageUnit storageUnit = new StorageUnit()
                                {
                                    UnitName = k.ToString(),
                                    StorageStripBoxId = latestStorageStripBoxId,
                                    IsOccupied = false
                                };
                                dbContext.StorageUnits.Add(storageUnit);
                            }
                            dbContext.SaveChanges();
                        }
                    }
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
        public async Task<List<StorageTankTypeDto>> GetStorageTankType()
        {
            var storageTankType = await dbContext.StorageTankTypes.Select(x => new StorageTankTypeDto
            {
                storageTankTypeId = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.storageTankTypeId).AsNoTracking().ToListAsync();
            return storageTankType;
        }

        private async Task<bool> HasTankName(string tankName)
        {
            return await dbContext.StorageTanks.AnyAsync(x=>x.TankName == tankName);
        }

        
        public async Task<List<OvumFreezeStorageDto>> GetOvumFreezeStorageInfo(Guid courseOfTreatmentId)
        {
            Guid customerId = await treatmentService.GetOvumOwnerCustomerId(courseOfTreatmentId);
            if (customerId == default(Guid))
            {
                return new List<OvumFreezeStorageDto>();
            }
            var result = await dbContext.OvumDetails.Where(x => x.CourseOfTreatment.CustomerId == customerId && x.OvumFreezeId != null && !x.OvumFreeze.IsThawed).GroupBy(x => x.OvumFreeze.StorageUnit.StorageStripBoxId).Select(x => new OvumFreezeStorageDto
            {
                stripBoxId = x.Key,
                tankId = x.Select(y => y.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.StorageTankId).FirstOrDefault(),
                tankInfo = new StorageTankDto
                {
                    tankName = x.Select(y => y.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.StorageTank.TankName).FirstOrDefault(),
                    tankTypeId = x.Select(y => y.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.StorageTank.StorageTankTypeId).FirstOrDefault()
                },
                canistId = x.Select(y => y.OvumFreeze.StorageUnit.StorageStripBox.StorageCanistId).FirstOrDefault(),
                canistName = x.Select(y => y.OvumFreeze.StorageUnit.StorageStripBox.StorageCanist.CanistName).FirstOrDefault(),
                stripBoxName = x.Select(y => y.OvumFreeze.StorageUnit.StorageStripBox.StripBoxName).FirstOrDefault(),
                storageUnitInfo = dbContext.StorageUnits.Where(y => y.StorageStripBoxId == x.Key).Select(y => new StorageUnitDto
                {
                    storageUnitId = y.SqlId,
                    unitName = y.UnitName,
                    isOccupied = y.IsOccupied
                }).ToList()
            }).ToListAsync();
            foreach (var i in result)
            {
                int count = 0;
                foreach (var j in i.storageUnitInfo)
                {
                    if (j.isOccupied == false)
                    {
                        count++;
                    }
                }
                i.stripBoxEmptyUnit = count;
            }
            return result;
        }
    }
}
