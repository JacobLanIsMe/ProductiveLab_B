using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForTransferIn;
using prjProductiveLab_B.Enums;
using prjProductiveLab_B.Interfaces;

namespace prjProductiveLab_B.Services
{
    public class TransferInService : ITransferInService
    {
        public BaseResponseDto AddTransferIn(AddTransferInDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                int cellType = 0;
                int.TryParse(input.transferInCellType, out cellType);
                
                switch(cellType)
                {
                    case (int)CellTypeEnum.ovum:
                        break;
                    default:
                        throw new Exception("請選擇要轉入'卵子', '胚胎' 還是 '精蟲'");
                        break;
                        
                }

            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }
        private void AddOvumTransferIn(AddTransferInDto input)
        {

        }
    }
}
