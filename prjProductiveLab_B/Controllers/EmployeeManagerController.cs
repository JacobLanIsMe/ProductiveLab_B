using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeManagerController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeeManagerController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        [HttpGet("GetAllEmbryologist")]
        public async Task<List<EmployeeDto>> GetAllEmbryologist()
        {
            return await employeeService.GetAllEmbryologist();
        }
        [HttpGet("GetAllDoctor")]
        public async Task<List<EmployeeDto>> GetAllDoctor()
        {
            return await employeeService.GetAllDoctor();
        }
    }
}
