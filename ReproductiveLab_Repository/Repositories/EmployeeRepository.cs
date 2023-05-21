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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ReproductiveLabContext _dbContext;
        public EmployeeRepository(ReproductiveLabContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Employee>> GetEmployeesByJobTitleId(JobTitleEnum jobTitle)
        {
            return await _dbContext.Employees.Where(x => x.JobTitleId == (int)jobTitle).ToListAsync();
        }
    }
}
