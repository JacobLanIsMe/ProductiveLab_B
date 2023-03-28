using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Models;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        public readonly ITreatmentService treatmentService;
        public readonly IOperateSpermService operateSpermService;
        public TreatmentController(ITreatmentService customerService, IOperateSpermService operateSpermService) 
        {
            this.treatmentService = customerService;
            this.operateSpermService = operateSpermService;
        }
        [HttpPost("AddOvumPickupNote")]
        public BaseResponseDto AddOvumPickupNote([FromBody] AddOvumPickupNoteDto ovumPickupNote)
        {
            return treatmentService.AddOvumPickupNote(ovumPickupNote);
        }

        [HttpGet("GetBaseTreatmentInfo")]
        public async Task<BaseTreatmentInfoDto> GetBaseTreatmentInfo(Guid courseOfTreatmentId)
        {
            return await treatmentService.GetBaseTreatmentInfo(courseOfTreatmentId);
        }

        [HttpGet("GetTreatmentSummary")]
        public async Task<List<TreatmentSummaryDto>> GetTreatmentSummary(Guid courseOfTreatmentId)
        {
            return await treatmentService.GetTreatmentSummary(courseOfTreatmentId);
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
    }
}
