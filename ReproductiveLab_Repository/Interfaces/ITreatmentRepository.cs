using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForTreatment;
using ReproductiveLab_Common.Models;
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
        void AddOvumDetail(AddOvumPickupNoteDto ovumPickupNote, Guid latestOvumPickupId, int ovumNumber);
        BaseTreatmentInfoDto? GetBaseTreatmentInfo(Guid courseOfTreatmentId);
        List<TreatmentSummaryModel> GetTreatmentSummary(Guid courseOfTreatmentId);
        List<Common1Dto> GetGermCellSituations();
        List<Common1Dto> GetGermCellSources();
        List<Common1Dto> GetGermCellOperations();
        List<Common1Dto> GetSpermRetrievalMethods();
        void AddCourseOfTreatment(AddCourseOfTreatmentDto input);
    }
}
