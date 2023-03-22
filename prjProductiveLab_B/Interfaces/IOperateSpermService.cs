using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Models;

namespace prjProductiveLab_B.Interfaces
{
    public interface IOperateSpermService
    {
        Task<BaseOperateSpermInfoDto> GetOriginInfoOfSperm(Guid courseOfTreatmentId);
        BaseResponseDto AddSpermScore(AddSpermScoreDto addSpermScore);
    }
}
