using prjProductiveLab_B.Dtos.ForTransferIn;
using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface ITransferInService
    {
        BaseResponseDto AddTransferIn(AddTransferInDto input);
    }
}
