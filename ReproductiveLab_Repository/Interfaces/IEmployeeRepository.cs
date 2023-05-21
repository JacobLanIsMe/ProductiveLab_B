using ReproductiveLab_Common.Enums;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesByJobTitleId(JobTitleEnum jobTitle);
    }
}
