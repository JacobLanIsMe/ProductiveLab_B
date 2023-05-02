using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface IAdminService
    {
        BaseResponseDto AddCustomer(AddCustomerDto input);
        Task<List<CommonDto>> GetGenders();
    }
}
