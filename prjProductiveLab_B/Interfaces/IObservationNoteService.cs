using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForObservationNote;

namespace prjProductiveLab_B.Interfaces
{
    public interface IObservationNoteService
    {
        Task<List<ObservationNoteDto>> GetObservationNote(Guid courseOfTreatmentId);
        Task<List<CommonDto>> GetOvumMaturation();
        Task<List<CommonDto>> GetObservationType();
        Task<List<CommonDto>> GetOvumAbnormality();
        Task<List<CommonDto>> GetFertilizationResult();
        Task<BlastomereScoreDto> GetBlastomereScore();
        Task<List<CommonDto>> GetEmbryoStatus();
        Task<BlastocystScoreDto> GetBlastocystScore();
        Task<List<CommonDto>> GetOperationType();
        Task<BaseResponseDto> AddObservationNote(AddObservationNoteDto input);
        Task<List<GetObservationNoteNameDto>> GetObservationNoteNameByObservationNoteIds(List<Guid> observationNoteIds);
        Task<GetObservationNoteDto?> GetExistingObservationNote(Guid observationNoteId);
        Task<GetObservationNoteNameDto?> GetExistingObservationNoteName(Guid observationNoteId);
        Task<BaseResponseDto> DeleteObservationNote(Guid observationNoteId);
        Task<BaseResponseDto> UpdateObservationNote(UpdateObservationNoteDto input);
        Task<List<GetObservationNoteNameDto>> GetFreezeObservationNotes(List<Guid> ovumDetailIds);
    }
}
