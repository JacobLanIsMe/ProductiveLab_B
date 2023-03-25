using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface IStorageService
    {
        Task<List<StorageTankStatusDot>> GetStorageTankStatus();
        Task<List<StorageUnitStatusDto>> GetStorageUnitStatus(int tankId, int shelfId);
        Task<BaseResponseDto> AddStorageTank(StorageAddNewTankDto storageAddNewTankDto);
        Task<List<StorageTankTypeDto>> GetStorageTankType();
    }
}
