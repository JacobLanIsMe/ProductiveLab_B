using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Models;

namespace prjProductiveLab_B.Services
{
    public class FunctionService : IFunctionService
    {
        private readonly ReproductiveLabContext dbContext;
        public FunctionService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<FunctionDto>> GetFunctions()
        {
            List<FunctionDto> functions = await dbContext.Functions.Select(x => new FunctionDto
            {
                functionId = x.SqlId,
                name = x.Name,
                route = x.Route,
            }).AsNoTracking().ToListAsync();
            return functions;
        }
    }
}
