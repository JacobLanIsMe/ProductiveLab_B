using ReproductiveLab_Common.Dtos;
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
        List<Common2Dto> GetEmployeesByJobTitleId(JobTitleEnum jobTitle);
    }
}
