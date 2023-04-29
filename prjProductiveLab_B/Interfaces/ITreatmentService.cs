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
        Task<BaseResponseDto> AddCourseOfTreatment(AddCourseOfTreatmentDto input);
        Task<BaseResponseDto> AddOvumFreeze(AddOvumFreezeDto input);
        Task<Guid> GetOvumOwnerCustomerId(Guid courseOfTreatmentId);
        Task<BaseCustomerInfoDto> GetOvumOwnerInfo(Guid courseOfTreatmentId);
        Task<List<CommonDto>> GetTopColors();
        Task<List<CommonDto>> GetFertilisationMethods();
        Task<List<CommonDto>> GetIncubators();
        BaseResponseDto AddFertilisation(AddFertilisationDto input);
    }
}
