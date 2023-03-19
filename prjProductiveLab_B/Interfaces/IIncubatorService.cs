using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface IIncubatorService
    {
        Task<IncubatorDto> GetAllIncubator();
    }
}
