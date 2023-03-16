using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionManagerController : ControllerBase
    {
        private readonly IFunctionService functionService;
        public FunctionManagerController(IFunctionService functionService)
        {
            this.functionService = functionService;
        }  
        [HttpGet("GetCommonFunctions")]
        public async Task<IEnumerable<FunctionDto>> GetCommonFunctions()
        {
            return await functionService.GetCommonFunctions();
        }
        [HttpGet("GetCaseSpecificFunctions")]
        public async Task<IEnumerable<FunctionDto>> GetCaseSpecificFunctions()
        {
            return await functionService.GetCaseSpecificFunctions();
        }
    }
}
