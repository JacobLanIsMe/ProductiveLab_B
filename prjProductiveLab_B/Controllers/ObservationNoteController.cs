using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using ReproductiveLab_Service.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservationNoteController : ControllerBase
    {
        private readonly IObservationNoteService _observationNoteService;
        public ObservationNoteController(IObservationNoteService observationNoteService)
        {
            _observationNoteService = observationNoteService;
        }
        [HttpGet("GetObservationNote")]
        public List<ObservationNoteDto> GetObservationNote(Guid courseOfTreatmentId)
        {
            return _observationNoteService.GetObservationNote(courseOfTreatmentId);
        }
        [HttpGet("GetOvumMaturation")]
        public List<Common1Dto> GetOvumMaturation()
        {
            return _observationNoteService.GetOvumMaturation();
        }
        [HttpGet("GetObservationType")]
        public List<Common1Dto> GetObservationType()
        {
            return _observationNoteService.GetObservationType();
        }
        [HttpGet("GetOvumAbnormality")]
        public List<Common1Dto> GetOvumAbnormality()
        {
            return _observationNoteService.GetOvumAbnormality();
        }
        [HttpGet("GetFertilizationResult")]
        public List<Common1Dto> GetFertilizationResult()
        {
            return _observationNoteService.GetFertilizationResult();
        }
        [HttpGet("GetBlastomereScore")]
        public async Task<BlastomereScoreDto> GetBlastomereScore()
        {
            return await _observationNoteService.GetBlastomereScore();
        }
        [HttpGet("GetEmbryoStatus")]
        public List<Common1Dto> GetEmbryoStatus()
        {
            return _observationNoteService.GetEmbryoStatus();
        }
        [HttpGet("GetBlastocystScore")]
        public async Task<BlastocystScoreDto> GetBlastocystScore()
        {
            return await _observationNoteService.GetBlastocystScore();
        }
        [HttpGet("GetOperationType")]
        public List<Common1Dto> GetOperationType()
        {
            return _observationNoteService.GetOperationType();
        }
        [HttpPost("AddObservationNote")]
        public BaseResponseDto AddObservationNote([FromForm] AddObservationNoteDto input)
        {
            return _observationNoteService.AddObservationNote(input);
        }
        [HttpGet("GetExistingObservationNote")]
        public GetObservationNoteDto? GetExistingObservationNote(Guid observationNoteId)
        {
            return _observationNoteService.GetExistingObservationNote(observationNoteId);
        }
        [HttpGet("GetExistingObservationNoteName")]
        public GetObservationNoteNameDto? GetExistingObservationNoteName(Guid observationNoteId)
        {
            return _observationNoteService.GetExistingObservationNoteName(observationNoteId);
        }
        [HttpGet("DeleteObservationNote")]
        public BaseResponseDto DeleteObservationNote(Guid observationNoteId)
        {
            return _observationNoteService.DeleteObservationNote(observationNoteId);
        }
        [HttpPost("UpdateObservationNote")]
        public BaseResponseDto UpdateObservationNote([FromForm] UpdateObservationNoteDto input)
        {
            return _observationNoteService.UpdateObservationNote(input);
        }
        [HttpPost("GetFreezeObservationNotes")]
        public List<GetObservationNoteNameDto> GetFreezeObservationNotes(List<Guid> ovumDetailIds)
        {
            return _observationNoteService.GetFreezeObservationNotes(ovumDetailIds);
        }
    }
}
