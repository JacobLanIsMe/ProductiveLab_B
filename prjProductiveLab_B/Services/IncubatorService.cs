using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;

namespace prjProductiveLab_B.Services
{
    public class IncubatorService : IIncubatorService
    {
        private readonly ReproductiveLabContext dbContext;
        public IncubatorService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IncubatorDto> GetAllIncubator()
        {
            var incubators = await dbContext.Incubators.Select(x=>new IncubatorViewModel
            {
                incubatorId = x.SqlId,
                name = x.Name,
            }).OrderBy(x=>x.incubatorId).AsNoTracking().ToListAsync();
            IncubatorDto result = new IncubatorDto();
            if (incubators.Count > 0)
            {
                result.data = incubators;
                result.SetSuccess();
            }
            else
            {
                result.SetError("無可用的培養箱");
            }
            return result;
        }
    }
}
