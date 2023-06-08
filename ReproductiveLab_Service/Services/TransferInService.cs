using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForTransferIn;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Service.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ReproductiveLab_Service.Services
{
    public class TransferInService : ITransferInService
    {
        private readonly ITransferInRepository _transferInRepository;
        private readonly IStorageRepository _storageRepository;
        public TransferInService(ITransferInRepository transferInRepository, IStorageRepository storageRepository)
        {
            _transferInRepository = transferInRepository;
            _storageRepository = storageRepository;
        }

        public BaseResponseDto AddTransferIn(AddTransferInDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    AddTransferInValidation(input);
                    _transferInRepository.AddTransferIn(input);
                    var storageUnits = _storageRepository.GetStorageUnitByIds(input.storageUnitIds);
                    foreach (var i in storageUnits)
                    {
                        _storageRepository.UpdateStorageUnitToOccupied(i);
                    }
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
