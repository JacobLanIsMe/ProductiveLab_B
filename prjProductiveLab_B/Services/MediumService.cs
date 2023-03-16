using prjProductiveLab_B.Dtos;
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
    }
}
