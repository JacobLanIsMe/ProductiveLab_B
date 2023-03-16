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
        public async Task<IEnumerable<FunctionDto>> GetCommonFunctions()
        {
            
            List<FunctionDto> allFunctions = await GetAllFunctions();
            IEnumerable<FunctionDto> commonFunctions = allFunctions.Where(x => x.functionTypeId == 1);
            return commonFunctions;
        }

        public async Task<IEnumerable<FunctionDto>> GetCaseSpecificFunctions()
        {

            List<FunctionDto> allFunctions = await GetAllFunctions();
            IEnumerable<FunctionDto> caseSpecificFunctions = allFunctions.Where(x => x.functionTypeId == 2);
            return caseSpecificFunctions;
        }

        public async Task<List<FunctionDto>> GetAllFunctions()
        {
            List<FunctionDto> allFunctions = await dbContext.Functions.Select(x => new FunctionDto
            {
                functionId = x.SqlId,
                name = x.Name,
                route = x.Route,
                functionTypeId = x.FunctionTypeId
            }).OrderBy(x=>x.functionId).AsNoTracking().ToListAsync();
            return allFunctions;
        }

        
    }
}
