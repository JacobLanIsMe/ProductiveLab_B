using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface IFreezeSummaryService
    {
        Task<GetOvumFreezeSummaryDto> GetOvumFreezeSummary(Guid courseOfTreatmentId);
    }
}
