using ReproductiveLab_Common.Dtos.ForOperateSperm;
using ReproductiveLab_Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Interfaces
{
    public interface IOperateSpermService
    {
        BaseResponseDto AddSpermScore(SpermScoreDto addSpermScore);
        BaseResponseDto UpdateExistingSpermScore(SpermScoreDto addSpermScore);
        List<Common1Dto> GetSpermFreezeOperationMethod();
        BaseResponseDto AddSpermFreeze(AddSpermFreezeDto input);
        List<SpermFreezeDto> GetSpermFreeze(int customerSqlId);
        List<SpermScoreDto> GetSpermScores(Guid courseOfTreatmentId);
        List<Common1Dto> GetSpermThawMethods();
        BaseResponseDto AddSpermThaw(AddSpermThawDto input);
        bool HasSpermFreezeByCourseOfTreatmentId(Guid courseOfTreatmentId);
    }
}
