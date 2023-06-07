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
        //IQueryable<OvumDetail> GetCustomerOvumFreeze(Guid customerId);
        //IQueryable<OvumDetail> GetRecipientOvumFreezes(Guid customerId);
    }
}
