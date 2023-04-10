using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservationNoteController : ControllerBase
    {
        private readonly IObservationNoteService observationNoteService;
        public ObservationNoteController(IObservationNoteService observationNoteService)
        {
            this.observationNoteService = observationNoteService;
        }
        [HttpGet("GetObservationNote")]
        public async Task<List<ObservationNoteDto>> GetObservationNote(Guid courseOfTreatmentId)
        {
            return await observationNoteService.GetObservationNote(courseOfTreatmentId);
        }
        [HttpGet("GetOvumMaturation")]
        public async Task<List<CommonDto>> GetOvumMaturation()
        {
            return await observationNoteService.GetOvumMaturation();
        }
        [HttpGet("GetObservationType")]
        public async Task<List<CommonDto>> GetObservationType()
        {
            return await observationNoteService.GetObservationType();
        }
        [HttpGet("GetOvumAbnormality")]
        public async Task<List<CommonDto>> GetOvumAbnormality()
        {
            return await observationNoteService.GetOvumAbnormality();
        }
        [HttpGet("GetFertilisationResult")]
        public async Task<List<CommonDto>> GetFertilisationResult()
        {
            return await observationNoteService.GetFertilisationResult();
        }
        [HttpGet("GetBlastomereScore")]
        public async Task<BlastomereScoreDto> GetBlastomereScore()
        {
            return await observationNoteService.GetBlastomereScore();
        }
        [HttpGet("GetEmbryoStatus")]
        public async Task<List<CommonDto>> GetEmbryoStatus()
        {
            return await observationNoteService.GetEmbryoStatus();
        }
        [HttpGet("GetBlastocystScore")]
        public async Task<BlastocystScoreDto> GetBlastocystScore()
        {
            return await observationNoteService.GetBlastocystScore();
        }
        [HttpGet("GetOperationType")]
        public async Task<List<CommonDto>> GetOperationType()
        {
            return await observationNoteService.GetOperationType();
        }
        [HttpPost("AddObservationNote")]
        public async Task<BaseResponseDto> AddObservationNote([FromForm] AddObservationNoteDto input)
        {
            return await observationNoteService.AddObservationNote(input);
        }
        [HttpGet("GetExistingObservationNote")]
        public async Task<GetObservationNoteDto?> GetExistingObservationNote(Guid observationNoteId)
        {
            return await observationNoteService.GetExistingObservationNote(observationNoteId);
        }
        [HttpGet("GetExistingObservationNoteName")]
        public async Task<GetObservationNoteNameDto?> GetExistingObservationNoteName(Guid observationNoteId)
        {
            return await observationNoteService.GetExistingObservationNoteName(observationNoteId);
        }
        [HttpGet("DeleteObservationNote")]
        public async Task<BaseResponseDto> DeleteObservationNote(Guid observationNoteId)
        {
            return await observationNoteService.DeleteObservationNote(observationNoteId);
        }
        [HttpPost("UpdateObservationNote")]
        public async Task<BaseResponseDto> UpdateObservationNote([FromForm] UpdateObservationNoteDto input)
        {
            return await observationNoteService.UpdateObservationNote(input);
        }
    }
}
