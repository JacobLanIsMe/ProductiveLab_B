using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForStorage;
using prjProductiveLab_B.Interfaces;

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
        public async Task<List<StorageTankStatusDto>> GetStorageTankStatus()
        {
            return await storageService.GetStorageTankStatus();
        }
        [HttpGet("GetStorageUnitStatus")]
        public async Task<List<StorageUnitStatusDto>> GetStorageUnitStatus(int tankId, int canistId)
        {
            return await storageService.GetStorageUnitStatus(tankId, canistId);
        }

        [HttpPost("AddStorageTank")]
        public async Task<BaseResponseDto> AddStorageTank(StorageAddNewTankDto storageAddNewTankDto)
        {
            return await storageService.AddStorageTank(storageAddNewTankDto);
        }

        [HttpGet("GetStorageTankType")]
        public async Task<List<StorageTankTypeDto>> GetStorageTankType()
        {
            return await storageService.GetStorageTankType();
        }
        [HttpGet("GetOvumFreezeStorageInfo")]
        public async Task<List<OvumFreezeStorageDto>> GetOvumFreezeStorageInfo(Guid courseOfTreatmentId)
        {
            return await storageService.GetOvumFreezeStorageInfo(courseOfTreatmentId);
        }
    }
}
