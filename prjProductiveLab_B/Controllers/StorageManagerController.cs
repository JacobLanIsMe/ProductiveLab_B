using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
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
        public async Task<List<StorageTankStatusDot>> GetStorageTankStatus()
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
            return await this.storageService.AddStorageTank(storageAddNewTankDto);
        }

        [HttpGet("GetStorageTankType")]
        public async Task<List<StorageTankTypeDto>> GetStorageTankType()
        {
            return await this.storageService.GetStorageTankType();
        }
    }
}
