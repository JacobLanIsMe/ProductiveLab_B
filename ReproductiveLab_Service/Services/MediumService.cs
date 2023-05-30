using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForMedium;
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
    public class MediumService : IMediumService
    {
        private readonly IMediumRepository _mediumRepository;
        public MediumService(IMediumRepository mediumRepository)
        {
            _mediumRepository = mediumRepository;
        }
        public BaseResponseDto AddMediumInUse(AddMediumInUseDto medium)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                AddMediumInUseValidation(medium);
                _mediumRepository.AddMediumInUse(medium);
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }
        private void AddMediumInUseValidation(AddMediumInUseDto medium)
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
        public List<InUseMediumDto> GetInUseMediums()
        {
            return _mediumRepository.GetInUseMediums();
        }
        public List<Common1Dto> GetMediumTypes()
        {
            return _mediumRepository.GetMediumTypes();
        }
        public List<FrequentlyUsedMediumDto> GetFrequentlyUsedMediums()
        {
            return _mediumRepository.GetFrequentlyUsedMediums();
        }
    }
}
