using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
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
        [HttpGet("GetExistingSpermScore")]
        public async Task<SpermScoreDto> GetExistingSpermScore(Guid spermFromCourseOfTreatmentId, int spermScoreTimePointId)
        {
            return await operateSpermService.GetExistingSpermScore(spermFromCourseOfTreatmentId, spermScoreTimePointId);
        }
        [HttpPut("UpdateExistingSpermScore")]
        public async Task<BaseResponseDto> UpdateExistingSpermScore(SpermScoreDto addSpermScore)
        {
            return await operateSpermService.UpdateExistingSpermScore(addSpermScore);
        }
        [HttpGet("GetSpermFreezeOperationMethod")]
        public async Task<List<SpermFreezeOperateMethodDto>> GetSpermFreezeOperationMethod()
        {
            return await operateSpermService.GetSpermFreezeOperationMethod();
        }
        [HttpPost("AddSpermFreeze")]
        public async Task<BaseResponseDto> AddSpermFreeze(AddSpermFreezeDto input)
        {
            return await operateSpermService.AddSpermFreeze(input);
        }
        [HttpPut("SelectSpermFreeze")]
        public async Task<BaseResponseDto> SelectSpermFreeze(List<int> unitIds)
        {
            return await operateSpermService.SelectSpermFreeze(unitIds);
        }
        [HttpGet("GetSpermFreeze")]
        public async Task<List<SpermFreezeDto>> GetSpermFreeze(Guid spermFromCourseOfTreatmentId)
        {
            return await operateSpermService.GetSpermFreeze(spermFromCourseOfTreatmentId);
        }
        [HttpGet("GetSpermScore")]
        public async Task<List<SpermScoreDto>> GetSpermScore(Guid spermFromCourseOfTreatmentId)
        {
            return await operateSpermService.GetSpermScore(spermFromCourseOfTreatmentId);
        }
    }
}
