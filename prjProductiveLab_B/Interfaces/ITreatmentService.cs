using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForTreatment;

namespace prjProductiveLab_B.Interfaces
{
    public interface ITreatmentService
    {
        BaseResponseDto AddOvumPickupNote(AddOvumPickupNoteDto ovumPickupNote);
        Task<BaseTreatmentInfoDto> GetBaseTreatmentInfo(Guid courseOfTreatmentId);
        Task<List<TreatmentSummaryDto>> GetTreatmentSummary(Guid courseOfTreatmentId);
        Task<List<TreatmentDto>> GetAllTreatment();
        Task<List<BaseCustomerInfoDto>> GetAllCustomer();
        Task<BaseCustomerInfoDto> GetCustomerByCustomerSqlId(int customerSqlId);
        Task<BaseCustomerInfoDto> GetCustomerByCourseOfTreatmentId(Guid courseOfTreatmentId);
        Task<BaseResponseDto> AddCourseOfTreatment(AddCourseOfTreatmentDto input);
        Task<BaseResponseDto> AddOvumFreeze(AddOvumFreezeDto input);
        Task<BaseCustomerInfoDto> GetOvumOwnerInfo(Guid ovumDetailId);
        Task<List<CommonDto>> GetTopColors();
        Task<List<CommonDto>> GetFertilisationMethods();
        Task<List<CommonDto>> GetIncubators();
        BaseResponseDto AddFertilisation(AddFertilisationDto input);
        BaseResponseDto AddOvumThaw(AddOvumThawDto input);
        Task<BaseResponseDto> OvumBankTransfer(OvumBankTransferDto input);
    }
}
