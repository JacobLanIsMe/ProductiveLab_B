using ReproductiveLab_Common.Models;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface IAdminRepository
    {
        void AddCustomer(CustomerModel input);
        Task<Customer?> GetLatestCustomer();
        Task<Customer?> GetCustomerById(Guid customerId);
        void UpdateSpouse(Customer customer, Guid spouseCustomerId);
        Task<List<Gender>> GetGenders();
    }
}
