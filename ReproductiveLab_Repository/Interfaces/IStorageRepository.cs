using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForStorage;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface IStorageRepository
    {
        List<StorageTankStatusDto> GetStorageTankStatus();
        List<StorageUnitStatusDto> GetStorageUnitStatus(int tankId, int canistId);
        bool HasTankName(string tankName);
        void AddStorageTank(StorageAddNewTankDto storageAddNewTankDto);
        int GetLatestStorageTankId();
        void AddStorageCanist(string canistName, int latestStorageTankId);
        int GetLatestStorageCanistId();
        void AddStorageStripBox(string stripBoxName, int latestStorageCanistId);
        int GetLatestStorageStripBoxId();
        void AddStorageUnit(string unitName, int latestStorageStripBoxId);
        List<StorageTankTypeDto> GetStorageTankType();
        List<OvumFreezeStorageDto> GetOvumFreezeStorageByCustomerId(Guid customerId);
        StorageUnit? GetStorageUnitById(int storageUnitId);
        IQueryable<StorageUnit> GetStorageUnitByIds(List<int> storageUnitIds);
        void UpdateStorageUnitToOccupied(StorageUnit storageUnit);
        List<Common1Dto> GetTopColors();
    }
}
