using prjProductiveLab_B.Dtos.ForFreezeSummary;

namespace prjProductiveLab_B.Interfaces
{
    public interface IFreezeSummaryService
    {
        Task<List<GetOvumFreezeSummaryDto>> GetOvumFreezeSummary(Guid courseOfTreatmentId);
        Task<List<GetSpermFreezeSummaryDto>> GetSpermFreezeSummary(Guid courseOfTreatmentId);
        Task<List<GetOvumFreezeSummaryDto>> GetRecipientOvumFreezes(Guid courseOfTreatmentId);
        Task<List<GetOvumFreezeSummaryDto>> GetDonorOvumFreezes(int customerSqlId);
        Task<List<GetOvumFreezeSummaryDto>> GetEmbryoFreezes(Guid courseOfTreatmentId);
    }
}
