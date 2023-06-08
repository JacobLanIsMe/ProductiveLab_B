using Microsoft.AspNetCore.Mvc;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForStorage;
using ReproductiveLab_Service.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageManagerController : ControllerBase
    {
        private readonly IStorageService storageService;
        public StorageManagerController(IStorageService storageService) 
        {
            this.storageService = storageService;
        }
        [HttpGet("GetStorageTankStatus")]
        public List<StorageTankStatusDto> GetStorageTankStatus()
        {
            return storageService.GetStorageTankStatus();
        }
        [HttpGet("GetStorageUnitStatus")]
        public List<StorageUnitStatusDto> GetStorageUnitStatus(int tankId, int canistId)
        {
            return storageService.GetStorageUnitStatus(tankId, canistId);
        }

        [HttpPost("AddStorageTank")]
        public BaseResponseDto AddStorageTank(StorageAddNewTankDto storageAddNewTankDto)
        {
            return storageService.AddStorageTank(storageAddNewTankDto);
        }

        [HttpGet("GetStorageTankType")]
        public List<StorageTankTypeDto> GetStorageTankType()
        {
            return storageService.GetStorageTankType();
        }
        [HttpGet("GetOvumFreezeStorageInfo")]
        public List<OvumFreezeStorageDto> GetOvumFreezeStorageInfo(Guid ovumDetailId)
        {
            return storageService.GetOvumFreezeStorageInfo(ovumDetailId);
        }
    }
}
