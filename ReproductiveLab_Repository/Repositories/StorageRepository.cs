using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForStorage;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly ReproductiveLabContext _db;
        public StorageRepository(ReproductiveLabContext db)
        {
            _db = db;
        }

        public List<StorageTankStatusDto> GetStorageTankStatus()
        {
            return _db.StorageUnits.GroupBy(x => new { x.StorageStripBox.StorageCanist.StorageTankId, x.StorageStripBox.StorageCanistId }).Select(y => new StorageTankStatusDto
            {
                tankId = y.Key.StorageTankId,
                tankInfo = _db.StorageTanks.Where(z => z.SqlId == y.Key.StorageTankId).Select(z => new StorageTankDto
                {
                    tankName = z.TankName,
                    tankTypeId = z.StorageTankTypeId
                }).FirstOrDefault(),
                canistId = y.Key.StorageCanistId,
                canistName = _db.StorageCanists.Where(z => z.SqlId == y.Key.StorageCanistId).Select(z => z.CanistName).FirstOrDefault(),
                emptyAmount = y.Where(z => z.IsOccupied == false).Count(),
                occupiedAmount = y.Where(z => z.IsOccupied == true).Count(),
                totalAmount = y.Count(),
                unitInfos = y.GroupBy(z => z.StorageStripBoxId).Select(a => new StorageUnitStatusDto
                {
                    stripIdOrBoxId = a.Key,
                    stripNameOrBoxName = _db.StorageStripBoxes.Where(b => b.SqlId == a.Key).Select(b => b.StripBoxName).FirstOrDefault(),
                    stripBoxEmptyUnit = a.Where(b => b.IsOccupied == false).Count(),
                    storageUnitInfo = a.Select(b => new StorageUnitDto
                    {
                        storageUnitId = b.SqlId,
                        unitName = b.UnitName,
                        isOccupied = b.IsOccupied
                    }).OrderBy(b => b.storageUnitId).ToList()
                }).OrderBy(a => a.stripIdOrBoxId).ToList()
            }).OrderBy(x => x.tankId).ThenBy(x => x.canistId).ToList();
        }
        public List<StorageUnitStatusDto> GetStorageUnitStatus(int tankId, int canistId)
        {
            return _db.StorageUnits.Where(x => x.StorageStripBox.StorageCanist.StorageTankId == tankId && x.StorageStripBox.StorageCanistId == canistId).GroupBy(x => x.StorageStripBoxId).Select(y => new StorageUnitStatusDto
            {
                stripIdOrBoxId = y.Key,
                stripNameOrBoxName = _db.StorageStripBoxes.Where(z => z.SqlId == y.Key).Select(z => z.StripBoxName).FirstOrDefault(),
                stripBoxEmptyUnit = y.Where(z => z.IsOccupied == false).Count(),
                storageUnitInfo = y.Select(z => new StorageUnitDto
                {
                    storageUnitId = z.SqlId,
                    unitName = z.UnitName,
                    isOccupied = z.IsOccupied,
                }).OrderBy(z => z.storageUnitId).ToList()
            }).OrderBy(y => y.stripIdOrBoxId).ToList();
        }
        public bool HasTankName(string tankName)
        {
            return _db.StorageTanks.Any(x => x.TankName == tankName);
        }
        public void AddStorageTank(StorageAddNewTankDto storageAddNewTankDto)
        {
            StorageTank storageTank = new StorageTank()
            {
                TankName = storageAddNewTankDto.tankName,
                StorageTankTypeId = storageAddNewTankDto.tankTypeId
            };
            _db.StorageTanks.Add(storageTank);
            _db.SaveChanges();
        }
        public int GetLatestStorageTankId()
        {
            return _db.StorageTanks.OrderByDescending(x => x.SqlId).Select(x => x.SqlId).FirstOrDefault();
        }
        public void AddStorageCanist(string canistName, int latestStorageTankId)
        {
            StorageCanist storageCanist = new StorageCanist()
            {
                CanistName = canistName,
                StorageTankId = latestStorageTankId
            };
            _db.StorageCanists.Add(storageCanist);
            _db.SaveChanges();
        }
        public int GetLatestStorageCanistId()
        {
            return _db.StorageCanists.OrderByDescending(x => x.SqlId).Select(x => x.SqlId).FirstOrDefault();
        }
        public void AddStorageStripBox(string stripBoxName, int latestStorageCanistId)
        {
            StorageStripBox storageStripBox = new StorageStripBox()
            {
                StripBoxName = stripBoxName,
                StorageCanistId = latestStorageCanistId
            };
            _db .StorageStripBoxes.Add(storageStripBox);
            _db.SaveChanges();
        }
        public int GetLatestStorageStripBoxId()
        {
            return _db.StorageStripBoxes.OrderByDescending(x => x.SqlId).Select(x => x.SqlId).FirstOrDefault();
        }
        public void AddStorageUnit(string unitName, int latestStorageStripBoxId)
        {
            StorageUnit storageUnit = new StorageUnit()
            {
                UnitName = unitName,
                StorageStripBoxId = latestStorageStripBoxId,
                IsOccupied = false
            };
            _db.StorageUnits.Add(storageUnit);
            _db.SaveChanges();
        }
        public List<StorageTankTypeDto> GetStorageTankType()
        {
            var storageTankType = _db.StorageTankTypes.Select(x => new StorageTankTypeDto
            {
                storageTankTypeId = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.storageTankTypeId).AsNoTracking().ToList();
            return storageTankType;
        }
        public List<OvumFreezeStorageDto> GetOvumFreezeStorageByCustomerId(Guid customerId)
        {
            return _db.OvumDetails.Where(x => x.CourseOfTreatment.CustomerId == customerId && x.OvumFreezeId != null && !x.OvumFreeze.IsThawed).GroupBy(x => x.OvumFreeze.StorageUnit.StorageStripBoxId).Select(x => new OvumFreezeStorageDto
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
                storageUnitInfo = _db.StorageUnits.Where(y => y.StorageStripBoxId == x.Key).Select(y => new StorageUnitDto
                {
                    storageUnitId = y.SqlId,
                    unitName = y.UnitName,
                    isOccupied = y.IsOccupied
                }).ToList()
            }).ToList();
        }
        public StorageUnit? GetStorageUnitById(int storageUnitId)
        {
            return _db.StorageUnits.FirstOrDefault(x => x.SqlId == storageUnitId);
        }
        public void UpdateStorageUnitToOccupied(StorageUnit storageUnit)
        {
            storageUnit.IsOccupied = true;
            _db.SaveChanges();
        }
        public List<Common1Dto> GetTopColors()
        {
            return _db.TopColors.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).ToList();
        }
    }
}
