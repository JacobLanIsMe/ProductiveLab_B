using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Service.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public List<Common2Dto> GetAllEmbryologist()
        {
            return _employeeRepository.GetEmployeesByJobTitleId(JobTitleEnum.embryologist);
        }
        public List<Common2Dto> GetAllDoctor()
        {
            return _employeeRepository.GetEmployeesByJobTitleId(JobTitleEnum.doctor);
        }
    }
}
