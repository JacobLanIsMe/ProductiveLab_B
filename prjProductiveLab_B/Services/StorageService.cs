using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Models;
using System.Transactions;

namespace prjProductiveLab_B.Services
{
    public class StorageService : IStorageService
    {
        private readonly ReproductiveLabContext dbContext;
        public StorageService(ReproductiveLabContext dbContext)
        { 
            this.dbContext = dbContext;
        }

        public async Task<List<StorageTankStatusDot>> GetStorageTankStatus()
        {
            var result = await dbContext.StorageUnits.GroupBy(x => new { x.StorageCaneBox.StorageShelf.StorageTankId, x.StorageCaneBox.StorageShelfId }).Select(y => new StorageTankStatusDot
            {
                tankId = y.Key.StorageTankId,
                tankInfo = dbContext.StorageTanks.Where(z => z.SqlId == y.Key.StorageTankId).Select(z => new StorageTankDto
                {
                    tankName = z.TankName,
                    tankTypeId = z.StorageTankTypeId
                }).FirstOrDefault(),
                shelfId = y.Key.StorageShelfId,
                shelfName = dbContext.StorageShelves.Where(z => z.SqlId == y.Key.StorageShelfId).Select(z => z.ShelfName).FirstOrDefault(),
                emptyAmount = y.Where(z => z.IsOccupied == false).Count(),
                occupiedAmount = y.Where(z => z.IsOccupied == true).Count(),
                totalAmount = y.Count()
            }).OrderBy(x=>x.tankId).ThenBy(x=>x.shelfId).ToListAsync();
            return result;
        }

        public async Task<List<StorageUnitStatusDto>> GetStorageUnitStatus(int tankId, int shelfId)
        {
            var result = await dbContext.StorageUnits.Where(x => x.StorageCaneBox.StorageShelf.StorageTankId == tankId && x.StorageCaneBox.StorageShelfId == shelfId).GroupBy(x => x.StorageCaneBoxId).Select(y => new StorageUnitStatusDto
            {
                caneIdOrBoxId = y.Key,
                caneNameOrBoxName = dbContext.StorageCaneBoxes.Where(z=>z.SqlId == y.Key).Select(z=>z.CaneBoxName).FirstOrDefault(),
                caneBoxEmptyUnit = y.Where(z=>z.IsOccupied == false).Count(),
                storageUnitInfo = y.Select(z => new StorageUnitDto
                {
                    storageUnitId = z.SqlId,
                    unitName = z.UnitName,
                    isOccupied = z.IsOccupied,
                }).OrderBy(z => z.storageUnitId).ToList()
            }).OrderBy(y => y.caneIdOrBoxId).ToListAsync();
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
                    for (int i = 1; i <= storageAddNewTankDto.shelfAmount; i++)
                    {
                        StorageShelf storageShelf = new StorageShelf()
                        {
                            ShelfName = i.ToString(),
                            StorageTankId = latestStorageTankId
                        };
                        dbContext.StorageShelves.Add(storageShelf);
                        dbContext.SaveChanges();
                        int latestStorageShelfId = dbContext.StorageShelves.Select(y => y.SqlId).OrderByDescending(y => y).FirstOrDefault();
                        for (int j = 1; j <= storageAddNewTankDto.caneBoxAmount; j++)
                        {
                            StorageCaneBox storageCaneBox = new StorageCaneBox()
                            {
                                CaneBoxName = j.ToString(),
                                StorageShelfId = latestStorageShelfId
                            };
                            dbContext.StorageCaneBoxes.Add(storageCaneBox);
                            dbContext.SaveChanges();
                            int latestStorageCanBoxId = dbContext.StorageCaneBoxes.Select(z => z.SqlId).OrderByDescending(z => z).FirstOrDefault();
                            for (int k = 1; k <= storageAddNewTankDto.unitAmount; k++)
                            {
                                StorageUnit storageUnit = new StorageUnit()
                                {
                                    UnitName = k.ToString(),
                                    StorageCaneBoxId = latestStorageCanBoxId,
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


    }
}
