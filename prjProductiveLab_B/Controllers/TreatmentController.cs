using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        public readonly ITreatmentService treatmentService;
        public TreatmentController(ITreatmentService customerService) 
        {
            this.treatmentService = customerService;
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
