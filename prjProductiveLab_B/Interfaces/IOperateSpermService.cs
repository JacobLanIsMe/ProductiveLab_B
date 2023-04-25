using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForOperateSperm;

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
        Task<List<CommonDto>> GetSpermThawMethods();
    }
}
