using Microsoft.EntityFrameworkCore;
using Reproductive_SharedFunction.Interfaces;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Models;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Service.Interfaces;
using System.Transactions;

namespace ReproductiveLab_Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customrRepository;
        private readonly IErrorFunction _errorFunctions;
        public CustomerService(ICustomerRepository customrRepository, IErrorFunction errorFunctions)
        {
            _customrRepository = customrRepository;
            _errorFunctions = errorFunctions;
        }
        public async Task<ResponseDto> AddCustomer(AddCustomerDto input)
        {
            ResponseDto result = new ResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _customrRepository.AddCustomer(new CustomerModel(input.name, input.genderId, input.birthday));
                    if (input.spouseName != null && input.spouseGenderId != null && input.spouseBirthday != null)
                    {
                        var latestCustomer = _customrRepository.GetLatestCustomer();
                        _errorFunctions.ThrowExceptionIfNull(latestCustomer, "Table Customer has no date");
                        Guid latestCustomerId = latestCustomer.CustomerId;
                        _customrRepository.AddCustomer(new CustomerModel(input.spouseName, (int)input.spouseGenderId, (DateTime)input.spouseBirthday, latestCustomerId));
                        var spouse = _customrRepository.GetLatestCustomer();
                        _errorFunctions.ThrowExceptionIfNull(spouse, "Insertion of spouse is failed");
                        Guid spouseCustomerId = spouse.CustomerId;
                        var customer = _customrRepository.GetCustomerById(latestCustomerId);
                        _errorFunctions.ThrowExceptionIfNull(customer, "Customer is not found");
                        _customrRepository.UpdateSpouse(customer, spouseCustomerId);
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
            var genders = _customrRepository.GetGenders();
            return genders.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.id).ToList();
        }
        public List<BaseCustomerInfoDto> GetAllCustomer()
        {
            return _customrRepository.GetAllCustomer();
        }
    }
}
