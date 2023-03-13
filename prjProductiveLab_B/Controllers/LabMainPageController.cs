using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Models;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabMainPageController : ControllerBase
    {
        private readonly ILabMainPage labMainPage;
        public LabMainPageController(ILabMainPage labMainPage)
        {
            this.labMainPage = labMainPage;        
        }
        // 回傳實驗室主畫面的資料
        [HttpGet("GetMainPageInfo")]
        public async Task<ActionResult<IEnumerable<LabMainPageDto>>> GetMainPageInfo()
        {
            IEnumerable<LabMainPageDto> result = await labMainPage.GetMainPageInfo();
            return Ok(result);
        }
    }
}
