using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;
using System.Runtime.CompilerServices;

namespace prjProductiveLab_B.Services
{
    public class FunctionService : IFunctionService
    {
        private readonly ReproductiveLabContext dbContext;
        public FunctionService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<FunctionDto>> GetAllFunctions()
        {
            List<FunctionDto> allFunctions = await dbContext.Functions.Where(x=>x.ParentFunctionId == 0).Select(x => new FunctionDto
            {
                functionId = x.SqlId,
                name = x.Name,
                route = x.Route,
                functionTypeId = x.FunctionTypeId,
                subFunctions = dbContext.Functions.Where(y=>y.ParentFunctionId == x.SqlId).Select(y=>new FunctionDto
                {
                    functionId = y.SqlId,
                    name = y.Name,
                    route= y.Route,
                    functionTypeId = y.FunctionTypeId,
                    subFunctions = null,
                }).AsNoTracking().ToList()
            }).OrderBy(x=>x.functionId).AsNoTracking().ToListAsync();
            return allFunctions;
        }

        public async Task<List<FunctionDto>> GetSubfunctions(int functionId)
        {
            return await dbContext.Functions.Where(x => x.ParentFunctionId == functionId).Select(x => new FunctionDto
            {
                functionId = x.SqlId,
                name = x.Name,
                route = x.Route,
                functionTypeId = x.FunctionTypeId,
                subFunctions = null
            }).OrderBy(x=>x.functionId).AsNoTracking().ToListAsync();
        }
    }
}
