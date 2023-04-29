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

        [HttpGet("GetAllFunctions")]
        public async Task<List<FunctionDto>> GetAllFunctions()
        {
            return await functionService.GetAllFunctions();
        }
        [HttpGet("GetSubfunctions")]
        public async Task<List<FunctionDto>> GetSubfunctions(int functionId)
        {
            return await functionService.GetSubfunctions(functionId);
        }
    }
}
