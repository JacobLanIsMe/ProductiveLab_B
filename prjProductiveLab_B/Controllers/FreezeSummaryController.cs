using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos.ForFreezeSummary;
using prjProductiveLab_B.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreezeSummaryController : ControllerBase
    {
        private readonly IFreezeSummaryService freezeSummaryService;
        public FreezeSummaryController(IFreezeSummaryService freezeSummaryService)
        {
            this.freezeSummaryService = freezeSummaryService;
        }
        [HttpGet("GetOvumFreezeSummary")]
        public async Task<List<GetOvumFreezeSummaryDto>> GetOvumFreezeSummary(Guid courseOfTreatmentId)
        {
            return await freezeSummaryService.GetOvumFreezeSummary(courseOfTreatmentId);
        }
        [HttpGet("GetSpermFreezeSummary")]
        public async Task<List<GetSpermFreezeSummaryDto>> GetSpermFreezeSummary(Guid courseOfTreatmentId)
        {
            return await freezeSummaryService.GetSpermFreezeSummary(courseOfTreatmentId);
        }
        [HttpGet("GetRecipientOvumFreezes")]
        public async Task<List<GetOvumFreezeSummaryDto>> GetRecipientOvumFreezes(Guid courseOfTreatmentId)
        {
            return await freezeSummaryService.GetRecipientOvumFreezes(courseOfTreatmentId);
        }
        [HttpGet("GetDonorOvums")]
        public async Task<List<GetOvumFreezeSummaryDto>> GetDonorOvums(int customerSqlId)
        {
            return await freezeSummaryService.GetDonorOvums(customerSqlId);
        }
        [HttpGet("GetEmbryoFreezes")]
        public async Task<List<GetOvumFreezeSummaryDto>> GetEmbryoFreezes(Guid courseOfTreatmentId)
        {
            return await freezeSummaryService.GetEmbryoFreezes(courseOfTreatmentId);
        }
        [HttpPost("GetUnFreezingObservationNoteOvumDetails")]
        public async Task<List<Guid>> GetUnFreezingObservationNoteOvumDetails(List<Guid> ovumDetailIds)
        {
            return await freezeSummaryService.GetUnFreezingObservationNoteOvumDetails(ovumDetailIds);
        }
    }
}
