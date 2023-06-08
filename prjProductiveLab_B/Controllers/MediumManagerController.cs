using Microsoft.AspNetCore.Mvc;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForMedium;
using ReproductiveLab_Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediumManagerController : ControllerBase
    {
        private readonly IMediumService _mediumService;
        public MediumManagerController(IMediumService mediumService)
        {
            _mediumService = mediumService;
        }

        [HttpPost("AddMediumInUse")]
        public BaseResponseDto AddMediumInUse(AddMediumInUseDto medium)
        {
            return _mediumService.AddMediumInUse(medium);            
        }
        [HttpGet("GetInUseMediums")]
        public List<InUseMediumDto> GetInUseMediums()
        {
            return _mediumService.GetInUseMediums();
        }
        [HttpGet("GetMediumTypes")]
        public List<Common1Dto> GetMediumTypes()
        {
            return _mediumService.GetMediumTypes();
        }
        [HttpGet("GetFrequentlyUsedMediums")]
        public List<FrequentlyUsedMediumDto> GetFrequentlyUsedMediums()
        {
            return _mediumService.GetFrequentlyUsedMediums();
        }
    }
}
