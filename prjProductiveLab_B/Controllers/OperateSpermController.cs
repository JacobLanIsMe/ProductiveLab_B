using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForOperateSperm;
using prjProductiveLab_B.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperateSpermController : ControllerBase
    {
        private readonly IOperateSpermService operateSpermService;
        public OperateSpermController(IOperateSpermService operateSpermService)
        {
            this.operateSpermService = operateSpermService;
        }
        [HttpGet("GetOriginInfoOfSperm")]
        public async Task<BaseOperateSpermInfoDto> GetOriginInfoOfSperm(Guid courseOfTreatmentId)
        {
            return await operateSpermService.GetOriginInfoOfSperm(courseOfTreatmentId);
        }
        [HttpPost("AddSpermScore")]
        public BaseResponseDto AddSpermScore(SpermScoreDto addSpermScore)
        {
            return operateSpermService.AddSpermScore(addSpermScore);
        }
        
        [HttpPut("UpdateExistingSpermScore")]
        public async Task<BaseResponseDto> UpdateExistingSpermScore(SpermScoreDto addSpermScore)
        {
            return await operateSpermService.UpdateExistingSpermScore(addSpermScore);
        }
        [HttpGet("GetSpermFreezeOperationMethod")]
        public async Task<List<CommonDto>> GetSpermFreezeOperationMethod()
        {
            return await operateSpermService.GetSpermFreezeOperationMethod();
        }
        [HttpPost("AddSpermFreeze")]
        public async Task<BaseResponseDto> AddSpermFreeze(AddSpermFreezeDto input)
        {
            return await operateSpermService.AddSpermFreeze(input);
        }
        [HttpGet("GetSpermFreeze")]
        public async Task<List<SpermFreezeDto>> GetSpermFreeze(Guid spermFromCourseOfTreatmentId)
        {
            return await operateSpermService.GetSpermFreeze(spermFromCourseOfTreatmentId);
        }
        [HttpGet("GetSpermScores")]
        public async Task<List<SpermScoreDto>> GetSpermScores(Guid courseOfTreatmentId)
        {
            return await operateSpermService.GetSpermScores(courseOfTreatmentId);
        }
        [HttpGet("GetSpermThawMethods")]
        public async Task<List<CommonDto>> GetSpermThawMethods()
        {
            return await operateSpermService.GetSpermThawMethods();
        }
        [HttpPost("AddSpermThaw")]
        public BaseResponseDto AddSpermThaw(AddSpermThawDto input)
        {
            return operateSpermService.AddSpermThaw(input);
        }
    }
}
