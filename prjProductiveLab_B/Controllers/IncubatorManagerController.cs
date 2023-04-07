using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncubatorManagerController : ControllerBase
    {
        private readonly IIncubatorService incubatorService;
        public IncubatorManagerController(IIncubatorService incubatorService)
        {
            this.incubatorService = incubatorService;
        }


        [HttpGet("GetAllIncubator")]
        public async Task<IncubatorDto> GetAllIncubator()
        {
            return await incubatorService.GetAllIncubator();
        }

    }
}
