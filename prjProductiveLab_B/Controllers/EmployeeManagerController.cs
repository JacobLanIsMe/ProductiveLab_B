using Microsoft.AspNetCore.Mvc;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Service.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeManagerController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeManagerController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet("GetAllEmbryologist")]
        public async Task<List<EmployeeDto>> GetAllEmbryologist()
        {
            return await _employeeService.GetAllEmbryologist();
        }
        [HttpGet("GetAllDoctor")]
        public async Task<List<EmployeeDto>> GetAllDoctor()
        {
            return await _employeeService.GetAllDoctor();
        }
    }
}
