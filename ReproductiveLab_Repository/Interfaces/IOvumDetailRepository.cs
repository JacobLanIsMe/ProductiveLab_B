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
        IQueryable<OvumDetail> GetCustomerOvumDetail(Guid customerId);
        //IQueryable<OvumDetail> GetCustomerOvumFreeze(Guid customerId);
        //IQueryable<OvumDetail> GetRecipientOvumFreezes(Guid customerId);
    }
}
