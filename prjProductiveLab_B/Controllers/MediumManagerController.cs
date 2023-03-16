using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Models;
using prjProductiveLab_B.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediumManagerController : ControllerBase
    {
        private readonly IMediumService mediumService;
        public MediumManagerController(IMediumService mediumService)
        {
            this.mediumService = mediumService;
        }

        [HttpPost("AddMedium")]
        public async Task<BaseResponseDto> AddMedium([FromBody] MediumInUse medium)
        {
            return await mediumService.AddMedium(medium);            
        }

        
    }
}
