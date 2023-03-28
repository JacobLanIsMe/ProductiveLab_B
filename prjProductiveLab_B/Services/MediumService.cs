using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Enums;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Models;

namespace prjProductiveLab_B.Services
{
    public class MediumService : IMediumService
    {
        private readonly ReproductiveLabContext dbContext;
        public MediumService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BaseResponseDto> AddMedium(MediumInUse medium)
        {
            BaseResponseDto baseResponse = new BaseResponseDto();
            try
            {
                await dbContext.MediumInUses.AddAsync(medium);
                dbContext.SaveChanges();
                baseResponse.SetSuccess();
            }
            catch(Exception ex)
            {
                baseResponse.SetError(ex.ToString());
            }
            return baseResponse;
        }
        public async Task<InUseMediumDto> GetInUseMedium(MediumTypeEnum mediumType)
        {
            InUseMediumDto result = new InUseMediumDto();
            List<InUseMedium> inUseMedium = await dbContext.MediumInUses.Where(x => x.IsDeleted == false && x.ExpirationDate >= DateTime.Now && x.MediumTypeId == (int)mediumType).Select(x => new InUseMedium
            {
                mediumInUseId = x.MediumInUseId.ToString(),
                name = x.Name,
                openDate = x.OpenDate,
                expirationDate = x.ExpirationDate,
                lotNumber = x.LotNumber,
                isDeleted = x.IsDeleted,
            }).OrderBy(x => x.expirationDate).AsNoTracking().ToListAsync();
            if (inUseMedium.Count > 0)
            {
                result.data = inUseMedium;
                result.SetSuccess();
            }
            else
            {
                result.SetError("無可用的培養液");
            }
            return result;
        }

    }
}
