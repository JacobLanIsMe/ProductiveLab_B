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
        public TreatmentController(ITreatmentService treatmentService, IOperateSpermService operateSpermService) 
        {
            this.treatmentService = treatmentService;
            this.operateSpermService = operateSpermService;
        }
        [HttpGet("GetAllTreatment")]
        public async Task<List<TreatmentDto>> GetAllTreatment()
        {
            return await this.treatmentService.GetAllTreatment();
        }
        [HttpPost("AddCourseOfTreatment")]
        public async Task<BaseResponseDto> AddCourseOfTreatment(AddCourseOfTreatmentDto input)
        {
            return await this.treatmentService.AddCourseOfTreatment(input);
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
       
        
    }
}
