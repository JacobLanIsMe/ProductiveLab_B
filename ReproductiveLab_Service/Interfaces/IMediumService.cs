using ReproductiveLab_Common.Dtos.ForMedium;
using ReproductiveLab_Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Interfaces
{
    public interface IMediumService
    {
        BaseResponseDto AddMediumInUse(AddMediumInUseDto medium);
        List<MediumDto> GetInUseMediums();
        List<Common1Dto> GetMediumTypes();
        List<FrequentlyUsedMediumDto> GetFrequentlyUsedMediums();
        List<MediumDto> GetInUseMediumByIds(List<Guid> mediumInUseIds);
    }
}
