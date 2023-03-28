using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Models;

namespace prjProductiveLab_B.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReproductiveLabContext dbContext;
        public EmployeeService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<EmployeeDto>> GetAllEmbryologist()
        {
            List<EmployeeDto> embryologist = await dbContext.Employees.Where(x=>x.JobTitleId == 2 && !x.IsDeleted).Select(x=>new EmployeeDto
            {
                employeeId = x.EmployeeId.ToString(),
                name = x.Name
            }).AsNoTracking().ToListAsync();
            return embryologist;
        }
    }
}
