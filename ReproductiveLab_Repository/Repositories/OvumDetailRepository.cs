using Microsoft.EntityFrameworkCore;
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
        private readonly ReproductiveLabContext _dbContext;
        public OvumDetailRepository(ReproductiveLabContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<OvumDetail> GetCustomerOvumDetail(Guid customerId)
        {
            return _dbContext.OvumDetails.Where(x => x.CourseOfTreatment.CustomerId == customerId);
        }
        
    }
}
