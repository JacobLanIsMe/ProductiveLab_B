using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
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
        private readonly ReproductiveLabContext _db;
        public EmployeeRepository(ReproductiveLabContext db)
        {
            _db = db;
        }
        public List<Common2Dto> GetEmployeesByJobTitleId(JobTitleEnum jobTitle)
        {
            return _db.Employees.Where(x => x.JobTitleId == (int)jobTitle).Select(x=>new Common2Dto
            {
                id = x.EmployeeId,
                name = x.Name,
            }).ToList();
        }
    }
}
