using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForTreatment;
using ReproductiveLab_Common.Models;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface ITreatmentRepository
    {
        void AddOvumPickup(AddOvumPickupNoteDto ovumPickupNote);
        Guid GetLatestOvumPickupId();
       
        BaseTreatmentInfoDto? GetBaseTreatmentInfo(Guid courseOfTreatmentId);
        List<TreatmentSummaryModel> GetTreatmentSummary(Guid courseOfTreatmentId);
        List<Common1Dto> GetGermCellSituations();
        List<Common1Dto> GetGermCellSources();
        List<Common1Dto> GetGermCellOperations();
        List<Common1Dto> GetSpermRetrievalMethods();
        List<Common1Dto> GetFertilizationMethods();
        List<Common1Dto> GetIncubators();
        void AddFertilization(AddFertilizationDto input);
        Guid GetLatestFertilizationId();
        void AddOvumThaw(AddOvumThawDto input);
        Guid GetLatestOvumThawId();
    }
}
