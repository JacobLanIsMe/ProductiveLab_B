using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Interfaces;
using ReproductiveLab_Common.Models;
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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ISharedFunction _sharedFunctions;
        public AdminService(IAdminRepository adminRepository, ISharedFunction sharedFunction)
        {
            _adminRepository = adminRepository;
            _sharedFunctions = sharedFunction;
        }
        public async Task<ResponseDto> AddCustomer(AddCustomerDto input)
        {
            ResponseDto result = new ResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _adminRepository.AddCustomer(new CustomerModel(input.name, input.genderId, input.birthday));
                    if (input.spouseName != null && input.spouseGenderId != null && input.spouseBirthday != null)
                    {
                        var latestCustomer = _adminRepository.GetLatestCustomer();
                        _sharedFunctions.ThrowExceptionIfNull(latestCustomer, "Table Customer has no date");
                        Guid latestCustomerId = latestCustomer.CustomerId;
                        _adminRepository.AddCustomer(new CustomerModel(input.spouseName, (int)input.spouseGenderId, (DateTime)input.spouseBirthday, latestCustomerId));
                        var spouse = _adminRepository.GetLatestCustomer();
                        _sharedFunctions.ThrowExceptionIfNull(spouse, "Insertion of spouse is failed");
                        Guid spouseCustomerId = spouse.CustomerId;
                        var customer = _adminRepository.GetCustomerById(latestCustomerId);
                        _sharedFunctions.ThrowExceptionIfNull(customer, "Customer is not found");
                        _adminRepository.UpdateSpouse(customer, spouseCustomerId);
                    }
                    scope.Complete();
                }
                result.SetSuccess();
            }
            catch(Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }

        public async Task<List<Common1Dto>> GetGenders()
        {
            var genders = _adminRepository.GetGenders();
            return genders.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.id).ToList();
        }
    }
}
