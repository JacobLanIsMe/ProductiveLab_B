using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Interfaces
{
    public interface IStorageService
    {
        List<StorageTankStatusDto> GetStorageTankStatus();
        List<StorageUnitStatusDto> GetStorageUnitStatus(int tankId, int canistId);
        BaseResponseDto AddStorageTank(StorageAddNewTankDto storageAddNewTankDto);
        List<StorageTankTypeDto> GetStorageTankType();
        List<OvumFreezeStorageDto> GetOvumFreezeStorageInfo(Guid ovumDetailId);
        List<Common1Dto> GetTopColors();
    }
}
