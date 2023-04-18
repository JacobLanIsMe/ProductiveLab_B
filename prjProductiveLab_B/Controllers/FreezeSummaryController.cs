using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
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
        public async Task<List<GetOvumFreezeSummaryDto>> GetOvumFreezeSummarys(Guid courseOfTreatmentId)
        {
            return await freezeSummaryService.GetOvumFreezeSummarys(courseOfTreatmentId);
        }
    }
}
