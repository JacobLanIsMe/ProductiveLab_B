using ReproductiveLab_Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Interfaces
{
    public interface ICustomerService
    {
        Task<ResponseDto> AddCustomer(AddCustomerDto input);
        Task<List<Common1Dto>> GetGenders();
        List<BaseCustomerInfoDto> GetAllCustomer();
        BaseCustomerInfoDto GetCustomerByCustomerSqlId(int customerSqlId);
        BaseCustomerInfoDto GetCustomerByCourseOfTreatmentId(Guid courseOfTreatmentId);
    }
}
