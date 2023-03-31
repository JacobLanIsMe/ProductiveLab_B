﻿using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Models;

namespace prjProductiveLab_B.Interfaces
{
    public interface IOperateSpermService
    {
        Task<BaseOperateSpermInfoDto> GetOriginInfoOfSperm(Guid courseOfTreatmentId);
        Task<List<SpermScoreDto>> GetSpermScore(Guid spermFromCourseOfTreatmentId);
        Task<List<SpermFreezeDto>> GetSpermFreeze(Guid spermFromCourseOfTreatmentId);
        BaseResponseDto AddSpermScore(SpermScoreDto addSpermScore);
        Task<SpermScoreDto> GetExistingSpermScore(Guid spermFromCourseOfTreatmentId, int spermScoreTimePointId);
        Task<BaseResponseDto> UpdateExistingSpermScore(SpermScoreDto addSpermScore);
        Task<List<SpermFreezeOperateMethodDto>> GetSpermFreezeOperationMethod();
        Task<BaseResponseDto> AddSpermFreeze(AddSpermFreezeDto input);
        Task<BaseResponseDto> SelectSpermFreeze(List<int> unitIds);
    }
}
