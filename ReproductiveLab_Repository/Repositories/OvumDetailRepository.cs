using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Enums;
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
    }
}
