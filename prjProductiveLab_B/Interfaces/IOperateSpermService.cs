using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface IOperateSpermService
    {
        Task<BaseOperateSpermInfoDto> GetOriginInfoOfSperm(Guid courseOfTreatmentId);
        Task<List<SpermScoreDto>> GetSpermScore(Guid courseOfTreatmentId);
        Task<List<SpermFreezeDto>> GetSpermFreeze(Guid spermFromCourseOfTreatmentId);
        BaseResponseDto AddSpermScore(SpermScoreDto addSpermScore);
        Task<BaseResponseDto> UpdateExistingSpermScore(SpermScoreDto addSpermScore);
        Task<List<CommonDto>> GetSpermFreezeOperationMethod();
        Task<BaseResponseDto> AddSpermFreeze(AddSpermFreezeDto input);
        Task<BaseResponseDto> SelectSpermFreeze(List<int> unitIds);
    }
}
