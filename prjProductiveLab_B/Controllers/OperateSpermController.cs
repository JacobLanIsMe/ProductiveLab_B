using Microsoft.AspNetCore.Mvc;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForOperateSperm;
using ReproductiveLab_Service.Interfaces;
//using prjProductiveLab_B.Dtos;
//using prjProductiveLab_B.Dtos.ForOperateSperm;
//using prjProductiveLab_B.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperateSpermController : ControllerBase
    {
        private readonly IOperateSpermService _operateSpermService;
        public OperateSpermController(IOperateSpermService operateSpermService)
        {
            _operateSpermService = operateSpermService;
        }
        [HttpPost("AddSpermScore")]
        public BaseResponseDto AddSpermScore(SpermScoreDto addSpermScore)
        {
            return _operateSpermService.AddSpermScore(addSpermScore);
        }
        [HttpPut("UpdateExistingSpermScore")]
        public BaseResponseDto UpdateExistingSpermScore(SpermScoreDto addSpermScore)
        {
            return _operateSpermService.UpdateExistingSpermScore(addSpermScore);
        }
        [HttpGet("GetSpermFreezeOperationMethod")]
        public List<Common1Dto> GetSpermFreezeOperationMethod()
        {
            return _operateSpermService.GetSpermFreezeOperationMethod();
        }
        [HttpPost("AddSpermFreeze")]
        public BaseResponseDto AddSpermFreeze(AddSpermFreezeDto input)
        {
            return _operateSpermService.AddSpermFreeze(input);
        }
        [HttpGet("GetSpermFreeze")]
        public List<SpermFreezeDto> GetSpermFreeze(int customerSqlId)
        {
            return _operateSpermService.GetSpermFreeze(customerSqlId);
        }
        [HttpGet("GetSpermScores")]
        public List<SpermScoreDto> GetSpermScores(Guid courseOfTreatmentId)
        {
            return _operateSpermService.GetSpermScores(courseOfTreatmentId);
        }
        [HttpGet("GetSpermThawMethods")]
        public List<Common1Dto> GetSpermThawMethods()
        {
            return _operateSpermService.GetSpermThawMethods();
        }
        [HttpPost("AddSpermThaw")]
        public BaseResponseDto AddSpermThaw(AddSpermThawDto input)
        {
            return _operateSpermService.AddSpermThaw(input);
        }
    }
}
