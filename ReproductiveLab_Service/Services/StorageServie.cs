using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForStorage;
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
    public class StorageServie : IStorageService
    {
        private readonly IStorageRepository _storageRepository;
        private readonly ICustomerRepository _customerRepository;
        public StorageServie(IStorageRepository storageRepository, ICustomerRepository customerRepository)
        {
            _storageRepository = storageRepository;
            _customerRepository = customerRepository;
        }

        public List<StorageTankStatusDto> GetStorageTankStatus()
        {
            return _storageRepository.GetStorageTankStatus();
        }
        public List<StorageUnitStatusDto> GetStorageUnitStatus(int tankId, int canistId)
        {
            return _storageRepository.GetStorageUnitStatus(tankId, canistId);
        }
        public BaseResponseDto AddStorageTank(StorageAddNewTankDto storageAddNewTankDto)
        {
            BaseResponseDto result = new BaseResponseDto();
            if (string.IsNullOrEmpty(storageAddNewTankDto.tankName))
            {
                result.SetError("液態氮桶的名稱未填");
                return result;
            }
            if (!string.IsNullOrEmpty(storageAddNewTankDto.tankName) && _storageRepository.HasTankName(storageAddNewTankDto.tankName))
            {
                result.SetError("液態氮桶的名稱已存在");
                return result;
            }

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _storageRepository.AddStorageTank(storageAddNewTankDto);
                    int latestStorageTankId = _storageRepository.GetLatestStorageTankId();
                    for (int i = 1; i <= storageAddNewTankDto.canistAmount; i++)
                    {
                        _storageRepository.AddStorageCanist(i.ToString(), latestStorageTankId);
                        int latestStorageCanistId = _storageRepository.GetLatestStorageCanistId();
                        for (int j = 1; j <= storageAddNewTankDto.stripBoxAmount; j++)
                        {
                            _storageRepository.AddStorageStripBox(j.ToString(), latestStorageCanistId);
                            int latestStorageStripBoxId = _storageRepository.GetLatestStorageStripBoxId();
                            for (int k = 1; k <= storageAddNewTankDto.unitAmount; k++)
                            {
                                _storageRepository.AddStorageUnit(k.ToString(), latestStorageStripBoxId);
                            }
                        }
                    }
                    scope.Complete();
                }
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }
        public List<StorageTankTypeDto> GetStorageTankType()
        {
            return _storageRepository.GetStorageTankType();
        }
        public List<OvumFreezeStorageDto> GetOvumFreezeStorageInfo(Guid ovumDetailId)
        {
            Guid customerId = _customerRepository.GetCustomerIdByOvumDetailId(ovumDetailId);
            if (customerId == Guid.Empty)
            {
                return new List<OvumFreezeStorageDto>();
            }
            var result = _storageRepository.GetOvumFreezeStorageByCustomerId(customerId);
            foreach (var i in result)
            {
                int count = 0;
                foreach (var j in i.storageUnitInfo)
                {
                    if (j.isOccupied == false)
                    {
                        count++;
                    }
                }
                i.stripBoxEmptyUnit = count;
            }
            return result;
        }
    }
}
