using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForTreatment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Interfaces
{
    public interface ITreatmentService
    {
        List<LabMainPageDto> GetMainPageInfo();
        List<Common1Dto> GetGermCellSituations();
        List<Common1Dto> GetGermCellSources();
        List<Common1Dto> GetGermCellOperations();
        List<Common1Dto> GetSpermRetrievalMethods();
        BaseResponseDto AddCourseOfTreatment(AddCourseOfTreatmentDto input);
        BaseResponseDto AddOvumPickupNote(AddOvumPickupNoteDto ovumPickupNote);
        BaseTreatmentInfoDto GetBaseTreatmentInfo(Guid courseOfTreatmentId);
        List<TreatmentSummaryDto> GetTreatmentSummary(Guid courseOfTreatmentId);
        BaseResponseDto AddOvumFreeze(AddOvumFreezeDto input);
        BaseResponseDto UpdateOvumFreeze(AddOvumFreezeDto input);
        AddOvumFreezeDto GetOvumFreeze(Guid ovumDetailId);
        BaseCustomerInfoDto GetOvumOwnerInfo(Guid ovumDetailId);
        List<Common1Dto> GetFertilizationMethods();
        List<Common1Dto> GetIncubators();
        BaseResponseDto AddFertilization(AddFertilizationDto input);
        BaseResponseDto AddOvumThaw(AddOvumThawDto input);
        BaseResponseDto OvumBankTransfer(OvumBankTransferDto input);
    }
}
