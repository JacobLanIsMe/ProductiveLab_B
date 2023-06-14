using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForMedium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface IMediumRepository
    {
        void AddMediumInUse(AddMediumInUseDto medium);
        List<MediumDto> GetInUseMediums();
        List<Common1Dto> GetMediumTypes();
        List<FrequentlyUsedMediumDto> GetFrequentlyUsedMediums();
        void SetMediumInUse<T>(T mediumTable, List<Guid> inputMediums);
        List<MediumDto> GetInUseMediumByIds(List<Guid> mediumInUseIds);
    }
}
