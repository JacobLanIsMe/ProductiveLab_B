using Microsoft.AspNetCore.Mvc;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForTreatment;
using ReproductiveLab_Service.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        private readonly ITreatmentService _treatmentService;
        private readonly ICustomerService _customerService;
        private readonly IStorageService _storageService;
        public readonly IOperateSpermService operateSpermService;
        public TreatmentController(ITreatmentService treatmentService, ICustomerService customerService, IStorageService storageService, IOperateSpermService operateSpermService) 
        {
            _treatmentService = treatmentService;
            _customerService = customerService;
            _storageService = storageService;
            this.operateSpermService = operateSpermService;
        }
       
        [HttpGet("GetGermCellSituations")]
        public List<Common1Dto> GetGermCellSituations()
        {
            return _treatmentService.GetGermCellSituations();
        }
        [HttpGet("GetGermCellSources")]
        public List<Common1Dto> GetGermCellSources()
        {
            return _treatmentService.GetGermCellSources();
        }
        [HttpGet("GetGermCellOperations")]
        public List<Common1Dto> GetGermCellOperations()
        {
            return _treatmentService.GetGermCellOperations();
        }
        [HttpGet("GetSpermRetrievalMethods")]
        public List<Common1Dto> GetSpermRetrievalMethods()
        {
            return _treatmentService.GetSpermRetrievalMethods();
        }
        [HttpGet("GetAllCustomer")]
        public List<BaseCustomerInfoDto> GetAllCustomer()
        {
            return _customerService.GetAllCustomer();
        }
        [HttpGet("GetCustomerByCustomerSqlId")]
        public BaseCustomerInfoDto GetCustomerByCustomerSqlId(int customerSqlId)
        {
            return _customerService.GetCustomerByCustomerSqlId(customerSqlId);
        }
        [HttpGet("GetCustomerByCourseOfTreatmentId")]
        public BaseCustomerInfoDto GetCustomerByCourseOfTreatmentId(Guid courseOfTreatmentId)
        {
            return _customerService.GetCustomerByCourseOfTreatmentId(courseOfTreatmentId);
        }
        [HttpPost("AddCourseOfTreatment")]
        public BaseResponseDto AddCourseOfTreatment(AddCourseOfTreatmentDto input)
        {
            return _treatmentService.AddCourseOfTreatment(input);
        }
        [HttpPost("AddOvumPickupNote")]
        public BaseResponseDto AddOvumPickupNote([FromBody] AddOvumPickupNoteDto ovumPickupNote)
        {
            return _treatmentService.AddOvumPickupNote(ovumPickupNote);
        }

        [HttpGet("GetBaseTreatmentInfo")]
        public BaseTreatmentInfoDto GetBaseTreatmentInfo(Guid courseOfTreatmentId)
        {
            return _treatmentService.GetBaseTreatmentInfo(courseOfTreatmentId);
        }

        [HttpGet("GetTreatmentSummary")]
        public List<TreatmentSummaryDto> GetTreatmentSummary(Guid courseOfTreatmentId)
        {
            return _treatmentService.GetTreatmentSummary(courseOfTreatmentId);
        }
        [HttpPost("AddOvumFreeze")]
        public BaseResponseDto AddOvumFreeze(AddOvumFreezeDto input)
        {
            return _treatmentService.AddOvumFreeze(input);
        }
        [HttpPost("UpdateOvumFreeze")]
        public BaseResponseDto UpdateOvumFreeze(AddOvumFreezeDto input)
        {
            return _treatmentService.UpdateOvumFreeze(input);
        }
        [HttpGet("GetOvumFreeze")]
        public AddOvumFreezeDto GetOvumFreeze(Guid ovumDetailId)
        {
            return _treatmentService.GetOvumFreeze(ovumDetailId);
        }
        [HttpGet("GetOvumOwnerInfo")]
        public BaseCustomerInfoDto GetOvumOwnerInfo(Guid ovumDetailId)
        {
            return _treatmentService.GetOvumOwnerInfo(ovumDetailId);
        }
        [HttpGet("GetTopColors")]
        public List<Common1Dto> GetTopColors()
        {
            return _storageService.GetTopColors();
        }
        [HttpGet("GetFertilizationMethods")]
        public List<Common1Dto> GetFertilizationMethods()
        {
            return _treatmentService.GetFertilizationMethods();
        }
        [HttpGet("GetIncubators")]
        public List<Common1Dto> GetIncubators()
        {
            return _treatmentService.GetIncubators();
        }
        [HttpPost("AddFertilization")]
        public BaseResponseDto AddFertilization(AddFertilizationDto input)
        {
            return _treatmentService.AddFertilization(input);
        }
        [HttpPost("AddOvumThaw")]
        public BaseResponseDto AddOvumThaw(AddOvumThawDto input)
        {
            return _treatmentService.AddOvumThaw(input);
        }
        [HttpPost("OvumBankTransfer")]
        public BaseResponseDto OvumBankTransfer(OvumBankTransferDto input)
        {
            return _treatmentService.OvumBankTransfer(input);
        }
    }
}
