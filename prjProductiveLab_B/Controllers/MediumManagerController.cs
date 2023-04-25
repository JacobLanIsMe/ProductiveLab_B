using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForMedium;
using prjProductiveLab_B.Enums;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;

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

        [HttpPost("AddMediumInUse")]
        public async Task<BaseResponseDto> AddMediumInUse(AddMediumInUseDto medium)
        {
            return await mediumService.AddMediumInUse(medium);            
        }
        [HttpGet("GetInUseMediums")]
        public async Task<List<InUseMediumDto>> GetInUseMediums()
        {
            return await mediumService.GetInUseMediums();
        }
        [HttpGet("GetMediumTypes")]
        public async Task<List<CommonDto>> GetMediumTypes()
        {
            return await mediumService.GetMediumTypes();
        }
        [HttpGet("GetFrequentlyUsedMediums")]
        public async Task<List<FrequentlyUsedMediumDto>> GetFrequentlyUsedMediums()
        {
            return await mediumService.GetFrequentlyUsedMediums();
        }
    }
}
