using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForStorage;

namespace prjProductiveLab_B.Interfaces
{
    public interface IStorageService
    {
        Task<List<StorageTankStatusDto>> GetStorageTankStatus();
        Task<List<StorageUnitStatusDto>> GetStorageUnitStatus(int tankId, int shelfId);
        Task<BaseResponseDto> AddStorageTank(StorageAddNewTankDto storageAddNewTankDto);
        Task<List<StorageTankTypeDto>> GetStorageTankType();
        Task<List<OvumFreezeStorageDto>> GetOvumFreezeStorageInfo(Guid ovumDetailId);
    }
}
