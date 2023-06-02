using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForOperateSperm;
using ReproductiveLab_Common.Models;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface IOperateSpermRepository
    {
        List<SpermFreezeDto> GetSpermFreeze(int customerSqlId);
        List<SpermScoreModel> GetSpermScoresByCourseOfTreatmentId(Guid courseOfTreatmentId);
        void AddSpermScore(SpermScoreDto addSpermScore);
        SpermScore? GetExistingSpermScoreByCourseOfTreatmentId(Guid courseOfTreatmentId, int spermScoreTimePointId);
        void UpdateSpermScore(SpermScoreDto addSpermScore, SpermScore existingSpermScore);
        List<Common1Dto> GetSpermFreezeOperationMethod();
        void AddSpermFreezeSituation(AddSpermFreezeDto input);
        Guid GetLatestSpermFreezeSituationId();
        void AddSpermFreeze(AddSpermFreezeDto input, Guid lastSituationId);
        List<Common1Dto> GetSpermThawMethods();
        void AddSpermThaw(AddSpermThawDto input);
        Guid GetLatestSpermThawId();
        void AddSpermThawFreezePair(List<Guid> spermFreezeIds, Guid latestSpermThawId);
    }
}
