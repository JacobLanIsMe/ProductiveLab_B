using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForOperateSperm;

namespace prjProductiveLab_B.Interfaces
{
    public interface IOperateSpermService
    {
        Task<List<SpermScoreDto>> GetSpermScores(Guid courseOfTreatmentId);
        Task<List<SpermFreezeDto>> GetSpermFreeze(int customerSqlId);
        BaseResponseDto AddSpermScore(SpermScoreDto addSpermScore);
        Task<BaseResponseDto> UpdateExistingSpermScore(SpermScoreDto addSpermScore);
        Task<List<CommonDto>> GetSpermFreezeOperationMethod();
        Task<BaseResponseDto> AddSpermFreeze(AddSpermFreezeDto input);
        Task<List<CommonDto>> GetSpermThawMethods();
        BaseResponseDto AddSpermThaw(AddSpermThawDto input);
    }
}
