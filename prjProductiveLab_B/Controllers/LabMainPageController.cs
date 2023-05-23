using Microsoft.AspNetCore.Mvc;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Service.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabMainPageController : ControllerBase
    {
        private readonly ITreatmentService _treatmentService;
        public LabMainPageController(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;        
        }
        // 回傳實驗室主畫面的資料
        [HttpGet("GetMainPageInfo")]
        public List<LabMainPageDto> GetMainPageInfo()
        {
            return _treatmentService.GetMainPageInfo();
        }
    }
}
