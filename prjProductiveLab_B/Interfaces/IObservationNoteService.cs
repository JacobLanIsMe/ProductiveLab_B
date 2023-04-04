using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface IObservationNoteService
    {
        Task<List<ObservationNoteDto>> GetObservationNote(Guid courseOfTreatmentId);
    }
}
