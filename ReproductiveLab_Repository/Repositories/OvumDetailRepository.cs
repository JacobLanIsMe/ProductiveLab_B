using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos.ForTreatment;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Common.Models;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Repositories
{
    public class OvumDetailRepository : IOvumDetailRepository
    {
        private readonly ReproductiveLabContext _db;
        public OvumDetailRepository(ReproductiveLabContext db)
        {
            _db = db;
        }
        public IQueryable<OvumDetail> GetOvumDetailByCustomerId(Guid customerId)
        {
            return _db.OvumDetails.Where(x => x.CourseOfTreatment.CustomerId == customerId);
        }
        public OvumDetail? GetFreezeOvumDetailByIds(List<Guid> ovumDetailIds)
        {
            return _db.OvumDetails.FirstOrDefault(x => ovumDetailIds.Contains(x.OvumDetailId) && x.OvumDetailStatusId == (int)OvumDetailStatusEnum.Freeze);
        }
        public OvumDetail? GetFreezeObservationOvumDetailByIds(List<Guid> ovumDetailIds)
        {
            return _db.OvumDetails.Where(x => ovumDetailIds.Contains(x.OvumDetailId)).Include(x => x.ObservationNotes).FirstOrDefault(x => !x.ObservationNotes.Any(y => y.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation));
        }
        public IQueryable<OvumDetail> GetOvumDetailByIds(List<Guid> ovumDetailIds)
        {
            return _db.OvumDetails.Where(x => ovumDetailIds.Contains(x.OvumDetailId));
        }
        public void UpdateOvumDetailToFreeze(IQueryable<OvumDetail> ovumDetails, Guid latestOvumFreezeId)
        {
            foreach (var i in ovumDetails)
            {
                i.OvumDetailStatusId = (int)OvumDetailStatusEnum.Freeze;
                i.OvumFreezeId = latestOvumFreezeId;
            }
            _db.SaveChanges();
        }
        public void UpdateOvumDetailToFertilization(IQueryable<OvumDetail> ovumDetails, Guid latestFertilizationId)
        {
            foreach (var i in ovumDetails)
            {
                i.FertilizationId = latestFertilizationId;
            }
            _db.SaveChanges();
        }
        public IQueryable<ThawedOvumDetailModel> GetThawOvumDetailByIds(List<Guid> ovumDetailIds)
        {
            return _db.OvumDetails.Where(x => ovumDetailIds.Contains(x.OvumDetailId) && x.OvumThawFreezePairFreezeOvumDetails.Any()).GroupBy(x => x.CourseOfTreatment.SqlId).Select(y => new ThawedOvumDetailModel
            {
                courseOfTreatmentSqlId = y.Key,
                ovumNumbers = y.Select(z => z.OvumNumber).OrderBy(z => z).ToList()
            });
        }
        public void AddOvumDetail(Guid courseOfTreatmentId, Guid OvumFromCourseOfTreatmentId, int ovumNumber, int ovumDetailStatusId, Guid? latestOvumPickupId = null, Guid? latestOvumThawId = null, Guid? fertilizationId = null)
        {
            OvumDetail ovumDetail = new OvumDetail()
            {
                CourseOfTreatmentId = courseOfTreatmentId,
                OvumFromCourseOfTreatmentId = OvumFromCourseOfTreatmentId,
                OvumNumber = ovumNumber,
                OvumDetailStatusId = ovumDetailStatusId
            };
            if (latestOvumPickupId.HasValue)
            {
                ovumDetail.OvumPickupId = latestOvumPickupId;
            }
            if (latestOvumThawId.HasValue)
            {
                ovumDetail.OvumThawId = latestOvumThawId;
            }
            if (fertilizationId.HasValue)
            {
                ovumDetail.FertilizationId = fertilizationId;
            }
            _db.OvumDetails.Add(ovumDetail);
            _db.SaveChanges();
        }
        public IQueryable<FreezeOvumDetailModel> GetFreezeOvumDetailModelByIds(List<Guid> ovumDetailIds)
        {
            return _db.OvumDetails.Where(x => x.OvumFreezeId != null && ovumDetailIds.Contains(x.OvumDetailId)).Select(x => new FreezeOvumDetailModel
            {
                ovumFreeze = x.OvumFreeze,
                storageUnit = x.OvumFreeze.StorageUnit,
                ovumDetail = x,
                observationNoteCount = x.ObservationNotes.Count,
                isTransferred = x.OvumTransferPairRecipientOvumDetails.Count > 0 ? true : false
            });
        }
        public void UpdateFreezeOvumDetail(IQueryable<FreezeOvumDetailModel> freezeOvumDetails, Guid latestOvumThawId)
        {
            
            foreach (var i in freezeOvumDetails)
            {
                i.ovumFreeze.IsThawed = true;
                i.storageUnit.IsOccupied = false;
                if (i.observationNoteCount == 0 && i.isTransferred)
                {
                    i.ovumDetail.OvumFreezeId = null;
                    i.ovumDetail.OvumDetailStatusId = (int)OvumDetailStatusEnum.Incubation;
                    i.ovumDetail.OvumThawId = latestOvumThawId;
                    OvumThawFreezePair pair = new OvumThawFreezePair
                    {
                        FreezeOvumDetailId = i.ovumDetail.OvumDetailId,
                        ThawOvumDetailId = i.ovumDetail.OvumDetailId
                    };
                    _db.OvumThawFreezePairs.Add(pair);
                }
            }
            _db.SaveChanges();
        }
    }
}
