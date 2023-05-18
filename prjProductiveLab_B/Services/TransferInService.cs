using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Dtos.ForTransferIn;
using prjProductiveLab_B.Enums;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;
using System.Transactions;

namespace prjProductiveLab_B.Services
{
    public class TransferInService : ITransferInService
    {
        private readonly ReproductiveLabContext dbContext;
        public TransferInService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public BaseResponseDto AddTransferIn(AddTransferInDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    AddTransferInValidation(input);
                    TransferIn transferIn = new TransferIn
                    {
                        TransferInTime = input.transferInTime
                    };
                    dbContext.TransferIns.Add(transferIn);
                    dbContext.SaveChanges();
                    var storageUnits = dbContext.StorageUnits.Where(x => input.storageUnitIds.Contains(x.SqlId));
                    foreach (var i in storageUnits)
                    {
                        i.IsOccupied = true;
                    }
                    dbContext.SaveChanges();
                    int cellType = 0;
                    int.TryParse(input.transferInCellType, out cellType);

                    switch (cellType)
                    {
                        case (int)CellTypeEnum.ovum:
                            AddOvumTransferIn(input);
                            break;
                        default:
                            throw new Exception("請選擇要轉入'卵子', '胚胎' 還是 '精蟲'");


                    }

                    scope.Complete();
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
            foreach (var i in input.storageUnitIds)
            {
                OvumFreeze ovumFreeze = new OvumFreeze
                {
                    FreezeTime = input.freezeTime,
                    Embryologist = input.embryologist,
                    StorageUnitId = i,
                    MediumInUseId = input.freezeMediumId,
                    OvumMorphologyA = 0,
                    OvumMorphologyB = 0,
                    OvumMorphologyC = 0,
                    TopColorId = 1,
                    IsThawed = false
                };
                
            }
            
        }
        private void AddTransferInValidation(AddTransferInDto input)
        {
            if (input.storageUnitIds == null || input.storageUnitIds.Count <= 0)
            {
                throw new Exception("請選擇儲位");
            }
        }
    }
}
