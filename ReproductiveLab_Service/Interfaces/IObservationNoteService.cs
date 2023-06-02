using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Interfaces
{
    public interface IObservationNoteService
    {
        List<ObservationNoteDto> GetObservationNote(Guid courseOfTreatmentId);
        List<Common1Dto> GetOvumMaturation();
        List<Common1Dto> GetObservationType();
        List<Common1Dto> GetOvumAbnormality();
        List<Common1Dto> GetFertilizationResult();
        Task<BlastomereScoreDto> GetBlastomereScore();
        List<Common1Dto> GetEmbryoStatus();
        Task<BlastocystScoreDto> GetBlastocystScore();
        List<Common1Dto> GetOperationType();
        BaseResponseDto AddObservationNote(AddObservationNoteDto input);
        GetObservationNoteDto? GetExistingObservationNote(Guid observationNoteId);
        List<GetObservationNoteNameDto> GetObservationNoteNameByObservationNoteIds(List<Guid> observationNoteIds);
        GetObservationNoteNameDto? GetExistingObservationNoteName(Guid observationNoteId);
        BaseResponseDto DeleteObservationNote(Guid observationNoteId);
        BaseResponseDto UpdateObservationNote(UpdateObservationNoteDto input);
        List<GetObservationNoteNameDto> GetFreezeObservationNotes(List<Guid> ovumDetailIds);
    }
}
