using Microsoft.EntityFrameworkCore;
using Reproductive_SharedFunction.Interfaces;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Models;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Repository.Repositories;
using ReproductiveLab_Service.Interfaces;
using System.Transactions;

namespace ReproductiveLab_Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IErrorFunction _errorFunctions;
        public CustomerService(ICustomerRepository customerRepository, IErrorFunction errorFunctions)
        {
            _customerRepository = customerRepository;
            _errorFunctions = errorFunctions;
        }
        public async Task<ResponseDto> AddCustomer(AddCustomerDto input)
        {
            ResponseDto result = new ResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _customerRepository.AddCustomer(new CustomerModel(input.name, input.genderId, input.birthday));
                    if (input.spouseName != null && input.spouseGenderId != null && input.spouseBirthday != null)
                    {
                        var latestCustomer = _customerRepository.GetLatestCustomer();
                        _errorFunctions.ThrowExceptionIfNull(latestCustomer, "Table Customer has no date");
                        Guid latestCustomerId = latestCustomer.CustomerId;
                        _customerRepository.AddCustomer(new CustomerModel(input.spouseName, (int)input.spouseGenderId, (DateTime)input.spouseBirthday, latestCustomerId));
                        var spouse = _customerRepository.GetLatestCustomer();
                        _errorFunctions.ThrowExceptionIfNull(spouse, "Insertion of spouse is failed");
                        Guid spouseCustomerId = spouse.CustomerId;
                        var customer = _customerRepository.GetCustomerById(latestCustomerId);
                        _errorFunctions.ThrowExceptionIfNull(customer, "Customer is not found");
                        _customerRepository.UpdateSpouse(customer, spouseCustomerId);
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
            var genders = _customerRepository.GetGenders();
            return genders.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.id).ToList();
        }
        public List<BaseCustomerInfoDto> GetAllCustomer()
        {
            return _customerRepository.GetAllCustomer();
        }
        public BaseCustomerInfoDto GetCustomerByCustomerSqlId(int customerSqlId)
        {
            return _customerRepository.GetBaseCustomerInfoBySqlId(customerSqlId);
        }
        public BaseCustomerInfoDto GetCustomerByCourseOfTreatmentId(Guid courseOfTreatmentId)
        {
            return _customerRepository.GetBaseCustomerInfoByCourseOfTreatmentId(courseOfTreatmentId);
        }
    }
}
