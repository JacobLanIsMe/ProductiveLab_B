using Microsoft.AspNetCore.Mvc;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Service.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionManagerController : ControllerBase
    {
        private readonly IFunctionService _functionService;
        public FunctionManagerController(IFunctionService functionService)
        {
            _functionService = functionService;
        } 

        [HttpGet("GetAllFunctions")]
        public List<FunctionDto> GetAllFunctions()
        {
            return _functionService.GetAllFunctions();
        }
        [HttpGet("GetSubfunctions")]
        public List<FunctionDto> GetSubfunctions(int functionId)
        {
            return _functionService.GetSubfunctions(functionId);
        }
    }
}
