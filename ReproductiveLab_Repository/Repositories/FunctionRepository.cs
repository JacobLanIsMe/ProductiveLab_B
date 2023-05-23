using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Repositories
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly ReproductiveLabContext _dbContext;
        public FunctionRepository(ReproductiveLabContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<FunctionDto> GetAllFunctions()
        {
            List<FunctionDto> allFunctions = _dbContext.Functions.Where(x => x.ParentFunctionId == 0).Select(x => new FunctionDto
            {
                functionId = x.SqlId,
                name = x.Name,
                route = x.Route,
                functionTypeId = x.FunctionTypeId,
                subFunctions = _dbContext.Functions.Where(y => y.ParentFunctionId == x.SqlId).Select(y => new FunctionDto
                {
                    functionId = y.SqlId,
                    name = y.Name,
                    route = y.Route,
                    functionTypeId = y.FunctionTypeId,
                    subFunctions = null,
                }).AsNoTracking().ToList()
            }).OrderBy(x => x.functionId).AsNoTracking().ToList();
            return allFunctions;
        }
        public List<FunctionDto> GetSubfunctions(int functionId)
        {
            return _dbContext.Functions.Where(x => x.ParentFunctionId == functionId).Select(x => new FunctionDto
            {
                functionId = x.SqlId,
                name = x.Name,
                route = x.Route,
                functionTypeId = x.FunctionTypeId,
                subFunctions = null
            }).OrderBy(x => x.functionId).AsNoTracking().ToList();
        }
    }
}
