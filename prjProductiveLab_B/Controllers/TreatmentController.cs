﻿using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForTreatment;
using prjProductiveLab_B.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        public readonly ITreatmentService treatmentService;
        public readonly IOperateSpermService operateSpermService;
        public TreatmentController(ITreatmentService treatmentService, IOperateSpermService operateSpermService) 
        {
            this.treatmentService = treatmentService;
            this.operateSpermService = operateSpermService;
        }
        [HttpGet("GetAllTreatment")]
        public async Task<List<TreatmentDto>> GetAllTreatment()
        {
            return await this.treatmentService.GetAllTreatment();
        }
        [HttpGet("GetAllCustomer")]
        public async Task<List<BaseCustomerInfoDto>> GetAllCustomer()
        {
            return await treatmentService.GetAllCustomer();
        }
        [HttpPost("AddCourseOfTreatment")]
        public async Task<BaseResponseDto> AddCourseOfTreatment(AddCourseOfTreatmentDto input)
        {
            return await this.treatmentService.AddCourseOfTreatment(input);
        }
        [HttpPost("AddOvumPickupNote")]
        public BaseResponseDto AddOvumPickupNote([FromBody] AddOvumPickupNoteDto ovumPickupNote)
        {
            return treatmentService.AddOvumPickupNote(ovumPickupNote);
        }

        [HttpGet("GetBaseTreatmentInfo")]
        public async Task<BaseTreatmentInfoDto> GetBaseTreatmentInfo(Guid courseOfTreatmentId)
        {
            return await treatmentService.GetBaseTreatmentInfo(courseOfTreatmentId);
        }

        [HttpGet("GetTreatmentSummary")]
        public async Task<List<TreatmentSummaryDto>> GetTreatmentSummary(Guid courseOfTreatmentId)
        {
            return await treatmentService.GetTreatmentSummary(courseOfTreatmentId);
        }
        [HttpPost("AddOvumFreeze")]
        public async Task<BaseResponseDto> AddOvumFreeze(AddOvumFreezeDto input)
        {
            return await treatmentService.AddOvumFreeze(input);
        }
        [HttpGet("GetOvumOwnerInfo")]
        public async Task<BaseCustomerInfoDto> GetOvumOwnerInfo(Guid courseOfTreatmentId)
        {
            return await treatmentService.GetOvumOwnerInfo(courseOfTreatmentId);
        }
        [HttpGet("GetTopColors")]
        public async Task<List<CommonDto>> GetTopColors()
        {
            return await treatmentService.GetTopColors();
        }
        [HttpGet("GetFertilisationMethods")]
        public async Task<List<CommonDto>> GetFertilisationMethods()
        {
            return await treatmentService.GetFertilisationMethods();
        }
        [HttpGet("GetIncubators")]
        public async Task<List<CommonDto>> GetIncubators()
        {
            return await treatmentService.GetIncubators();
        }
        [HttpPost("AddFertilisation")]
        public BaseResponseDto AddFertilisation(AddFertilisationDto input)
        {
            return treatmentService.AddFertilisation(input);
        }
        [HttpPost("AddOvumThaw")]
        public BaseResponseDto AddOvumThaw(AddOvumThawDto input)
        {
            return treatmentService.AddOvumThaw(input);
        }
    }
}
