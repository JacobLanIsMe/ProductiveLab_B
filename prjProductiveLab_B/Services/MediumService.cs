using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Enums;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;
using System.Transactions;

namespace prjProductiveLab_B.Services
{
    public class MediumService : IMediumService
    {
        private readonly ReproductiveLabContext dbContext;
        public MediumService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BaseResponseDto> AddMediumInUse(AddMediumInUseDto medium)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    AddMediumInUseValidation(medium);
                    string? mediumName = "";
                    int? mediumTypeId = 0;
                    if (medium.frequentlyUsedMediumId != 0)
                    {
                        var frequentlyUsedMedium = dbContext.FrequentlyUsedMedia.FirstOrDefault(x => x.SqlId == medium.frequentlyUsedMediumId);
                        if (frequentlyUsedMedium == null)
                        {
                            throw new Exception("找不到此常用的培養液名稱");
                        }
                        mediumName = frequentlyUsedMedium.Name;
                    }
                    else
                    {
                        mediumName = medium.customizedMedium;
                    }
                    if (medium.mediumTypeId != 0)
                    {
                        mediumTypeId = medium.mediumTypeId;
                    }
                    else
                    {
                        mediumTypeId = null;
                    }
                    MediumInUse mediumInUse = new MediumInUse
                    {
                        Name = mediumName,
                        OpenDate = medium.openDate,
                        ExpirationDate = medium.expirationDate,
                        LotNumber = medium.lotNumber,
                        IsDeleted = false,
                        MediumTypeId = mediumTypeId,
                    };

                    dbContext.MediumInUses.Add(mediumInUse);
                    dbContext.SaveChanges();
                    scope.Complete();
                }
                result.SetSuccess();
            }
            catch(Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }
        public void AddMediumInUseValidation(AddMediumInUseDto medium)
        {
            if (medium.frequentlyUsedMediumId == 0 && medium.customizedMedium == null) 
            {
                throw new Exception("培養液名稱不能為空");
            }
            if (medium.lotNumber == null)
            {
                throw new Exception("Lot Number 不能為空");
            }
        }
        public async Task<List<InUseMediumDto>> GetInUseMediums()
        {
            return await dbContext.MediumInUses.Where(x=>x.IsDeleted == false && x.ExpirationDate >= DateTime.Now).Select(x=>new InUseMediumDto
            {
                mediumInUseId = x.MediumInUseId.ToString(),
                name = x.Name,
                openDate = x.OpenDate,
                expirationDate = x.ExpirationDate,
                lotNumber = x.LotNumber,
                isDeleted = x.IsDeleted,
                mediumTypeId = x.MediumTypeId,
            }).OrderBy(x=>x.mediumTypeId).ThenBy(x=>x.name).AsNoTracking().ToListAsync();
        }
        public async Task<List<CommonDto>> GetMediumTypes()
        {
            return await dbContext.MediumTypes.Where(x=>x.SqlId != (int)MediumTypeEnum.other).Select(x=>new CommonDto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x=>x.id).AsNoTracking().ToListAsync();
        }
        public async Task<List<FrequentlyUsedMediumDto>> GetFrequentlyUsedMediums()
        {
            return await dbContext.FrequentlyUsedMedia.Select(x => new FrequentlyUsedMediumDto
            {
                id = x.SqlId,
                name = x.Name,
                mediumTypeId = x.MediumTypeId,
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
    }
}
