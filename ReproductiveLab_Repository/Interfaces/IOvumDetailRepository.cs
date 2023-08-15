using ReproductiveLab_Common.Dtos.ForFreezeSummary;
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
    public interface IOvumDetailRepository
    {
        IQueryable<OvumDetail> GetOvumDetailByCustomerId(Guid customerId);
        OvumDetail? GetFreezeOvumDetailByIds(List<Guid> ovumDetailIds);
        OvumDetail? GetFreezeObservationOvumDetailByIds(List<Guid> ovumDetailIds);
        IQueryable<OvumDetail> GetOvumDetailByIds(List<Guid> ovumDetailIds);
        void UpdateOvumDetailToFreeze(IQueryable<OvumDetail> ovumDetails, Guid latestOvumFreezeId);
        void UpdateOvumDetailToFertilization(IQueryable<OvumDetail> ovumDetails, Guid latestFertilizationId);
        IQueryable<ThawedOvumDetailModel> GetThawOvumDetailByIds(List<Guid> ovumDetailIds);
        void AddOvumDetail(Guid courseOfTreatmentId, Guid OvumFromCourseOfTreatmentId, int ovumNumber, int ovumDetailStatusId, Guid? latestOvumPickupId = null, Guid? latestOvumThawId = null, Guid? fertilizationId = null, Guid? ovumFreezeId = null);
        void UpdateFreezeOvumDetail(IQueryable<FreezeOvumDetailModel> freezeOvumDetails, Guid latestOvumThawId);
        IQueryable<FreezeOvumDetailModel> GetFreezeOvumDetailModelByIds(List<Guid> ovumDetailIds);
        Guid GetLatestOvumDetailId();
        List<GetOvumFreezeSummaryDto> GetBaseOvumDetailInfosByOvumDetails(IQueryable<OvumDetail> ovumDetails);
    }
}
