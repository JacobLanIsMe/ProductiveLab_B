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
        List<InUseMediumDto> GetInUseMediums();
        List<Common1Dto> GetMediumTypes();
        List<FrequentlyUsedMediumDto> GetFrequentlyUsedMediums();
        void SetMediumInUse<T>(T mediumTable, List<Guid> inputMediums);
    }
}
