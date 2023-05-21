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
        public async Task<List<EmployeeDto>> GetAllEmbryologist()
        {
            var embryologists = await _employeeRepository.GetEmployeesByJobTitleId(JobTitleEnum.embryologist);
            return ConvertEmployeeToEmployeeDto(embryologists);
        }
        public async Task<List<EmployeeDto>> GetAllDoctor()
        {
            var doctors = await _employeeRepository.GetEmployeesByJobTitleId(JobTitleEnum.doctor);
            return ConvertEmployeeToEmployeeDto(doctors);
        }
        private List<EmployeeDto> ConvertEmployeeToEmployeeDto(List<Employee> employees)
        {
            return employees.Select(x => new EmployeeDto
            {
                employeeId = x.EmployeeId,
                name = x.Name
            }).ToList();
        }
    }
}
