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
    }
}
