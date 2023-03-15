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
        public async Task<List<FunctionDto>> GetCommonFunctions()
        {
            
            List<FunctionTypeDto> allFunctions = await GetAllFunctions();
            IEnumerable<FunctionTypeDto> commonFunctions = allFunctions.Where(x => x.functionTypeId == 1);
            List<FunctionDto> result = TransformToFunctionDto(commonFunctions);
            return result;
        }

        public async Task<List<FunctionDto>> GetCaseSpecificFunctions()
        {

            List<FunctionTypeDto> allFunctions = await GetAllFunctions();
            IEnumerable<FunctionTypeDto> caseSpecificFunctions = allFunctions.Where(x => x.functionTypeId == 2);
            List<FunctionDto> result = TransformToFunctionDto(caseSpecificFunctions);
            return result;
        }

        public async Task<List<FunctionTypeDto>> GetAllFunctions()
        {
            List<FunctionTypeDto> allFunctions = await dbContext.Functions.Select(x => new FunctionTypeDto
            {
                functionId = x.SqlId,
                name = x.Name,
                route = x.Route,
                functionTypeId = x.FunctionTypeId
            }).AsNoTracking().ToListAsync();
            return allFunctions;
        }

        public List<FunctionDto> TransformToFunctionDto(IEnumerable<FunctionTypeDto> allFunctions)
        {
            List<FunctionDto> result = new List<FunctionDto>();
            foreach (FunctionTypeDto function in allFunctions)
            {
                result.Add((FunctionDto)function);
            }
            return result;
        }
    }
}
