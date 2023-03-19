using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface ITreatmentService
    {
        BaseResponseDto AddOvumPickupNote(AddOvumPickupNote ovumPickupNote);
        Task<BaseTreatmentInfoDto> GetBaseTreatmentInfo(Guid courseOfTreatmentId);
    }
}
