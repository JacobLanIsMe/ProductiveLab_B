using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Service.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerService _adminService;
        public AdminController(ICustomerService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost("AddCustomer")]
        public async Task<ResponseDto> AddCustomer(AddCustomerDto input)
        {
            return await _adminService.AddCustomer(input);
        }
        [HttpGet("GetGenders")]
        public async Task<List<Common1Dto>> GetGenders()
        {
            return await _adminService.GetGenders();
        }
    }
}
