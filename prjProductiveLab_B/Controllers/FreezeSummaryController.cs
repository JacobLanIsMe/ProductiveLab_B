using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using ReproductiveLab_Service.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreezeSummaryController : ControllerBase
    {
        private readonly IFreezeSummaryService _freezeSummaryService;
        public FreezeSummaryController(IFreezeSummaryService freezeSummaryService)
        {
            _freezeSummaryService = freezeSummaryService;
        }
        [HttpGet("GetOvumFreezeSummary")]
        public List<GetOvumFreezeSummaryDto> GetOvumFreezeSummary(Guid courseOfTreatmentId)
        {
            return _freezeSummaryService.GetOvumFreezeSummary(courseOfTreatmentId);
        }
        [HttpGet("GetSpermFreezeSummary")]
        public List<GetSpermFreezeSummaryDto> GetSpermFreezeSummary(Guid courseOfTreatmentId)
        {
            return _freezeSummaryService.GetSpermFreezeSummary(courseOfTreatmentId);
        }
        [HttpGet("GetRecipientOvumFreezes")]
        public List<GetOvumFreezeSummaryDto> GetRecipientOvumFreezes(Guid courseOfTreatmentId)
        {
            return _freezeSummaryService.GetRecipientOvumFreezes(courseOfTreatmentId);
        }
        [HttpGet("GetDonorOvums")]
        public List<GetOvumFreezeSummaryDto> GetDonorOvums(int customerSqlId)
        {
            return _freezeSummaryService.GetDonorOvums(customerSqlId);
        }
        [HttpGet("GetEmbryoFreezes")]
        public List<GetOvumFreezeSummaryDto> GetEmbryoFreezes(Guid courseOfTreatmentId)
        {
            return _freezeSummaryService.GetEmbryoFreezes(courseOfTreatmentId);
        }
        [HttpPost("GetUnFreezingObservationNoteOvumDetails")]
        public List<Guid> GetUnFreezingObservationNoteOvumDetails(List<Guid> ovumDetailIds)
        {
            return _freezeSummaryService.GetUnFreezingObservationNoteOvumDetails(ovumDetailIds);
        }
    }
}
