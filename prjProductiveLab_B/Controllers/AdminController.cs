using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [HttpPost("AddCustomer")]
        public BaseResponseDto AddCustomer(AddCustomerDto input)
        {
            return adminService.AddCustomer(input);
        }
        [HttpGet("GetGenders")]
        public async Task<List<CommonDto>> GetGenders()
        {
            return await adminService.GetGenders();
        }
    }
}
