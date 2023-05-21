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
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
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
