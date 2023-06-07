using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Models;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Guid? GetCustomerIdByCourseOfTreatmentId(Guid courseOfTreatmentId);
        void AddCustomer(CustomerModel input);
        Customer? GetLatestCustomer();
        Customer? GetCustomerById(Guid customerId);
        void UpdateSpouse(Customer customer, Guid spouseCustomerId);
        List<Gender> GetGenders();
        Customer? GetCustomerBySqlId(int customerSqlId);
        Guid GetCustomerIdByOvumDetailId(Guid ovumDetailId);
        List<BaseCustomerInfoDto> GetAllCustomer();
        BaseCustomerInfoDto GetBaseCustomerInfoBySqlId(int customerSqlId);
        BaseCustomerInfoDto GetBaseCustomerInfoByCourseOfTreatmentId(Guid courseOfTreatmentId);
        BaseCustomerInfoDto? GetBaseCustomerInfoByOvumDetailId(Guid ovumDetailId);
    }
}
