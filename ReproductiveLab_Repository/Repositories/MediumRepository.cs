using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForMedium;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ReproductiveLab_Repository.Repositories
{
    public class MediumRepository : IMediumRepository
    {
        private readonly ReproductiveLabContext _db;
        public MediumRepository(ReproductiveLabContext db)
        {
            _db = db;
        }

        public void AddMediumInUse(AddMediumInUseDto medium)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string? mediumName = "";
                    int? mediumTypeId = 0;
                    if (medium.frequentlyUsedMediumId != 0)
                    {
                        var frequentlyUsedMedium = _db.FrequentlyUsedMedia.FirstOrDefault(x => x.SqlId == medium.frequentlyUsedMediumId);
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

                    _db.MediumInUses.Add(mediumInUse);
                    _db.SaveChanges();
                    scope.Complete();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public List<InUseMediumDto> GetInUseMediums()
        {
            return _db.MediumInUses.Where(x => x.IsDeleted == false && x.ExpirationDate >= DateTime.Now).Select(x => new InUseMediumDto
            {
                mediumInUseId = x.MediumInUseId.ToString(),
                name = x.Name,
                openDate = x.OpenDate,
                expirationDate = x.ExpirationDate,
                lotNumber = x.LotNumber,
                isDeleted = x.IsDeleted,
                mediumTypeId = x.MediumTypeId,
            }).OrderBy(x => x.mediumTypeId).ThenBy(x => x.name).AsNoTracking().ToList();
        }
        public List<Common1Dto> GetMediumTypes()
        {
            return _db.MediumTypes.Where(x => x.SqlId != (int)MediumTypeEnum.other).Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public List<FrequentlyUsedMediumDto> GetFrequentlyUsedMediums()
        {
            return _db.FrequentlyUsedMedia.Select(x => new FrequentlyUsedMediumDto
            {
                id = x.SqlId,
                name = x.Name,
                mediumTypeId = x.MediumTypeId,
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
    }
}
