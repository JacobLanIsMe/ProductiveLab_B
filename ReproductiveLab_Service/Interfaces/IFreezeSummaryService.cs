using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Interfaces
{
    public interface IFreezeSummaryService
    {
        List<GetOvumFreezeSummaryDto> GetOvumFreezeSummary(Guid courseOfTreatmentId);
        List<GetOvumFreezeSummaryDto> GetRecipientOvumFreezes(Guid courseOfTreatmentId);
        List<GetOvumFreezeSummaryDto> GetDonorOvums(int customerSqlId);
        List<GetOvumFreezeSummaryDto> GetEmbryoFreezes(Guid courseOfTreatmentId);
        List<Guid> GetUnFreezingObservationNoteOvumDetails(List<Guid> ovumDetailIds);
        List<GetSpermFreezeSummaryDto> GetSpermFreezeSummary(Guid courseOfTreatmentId);
    }
}
