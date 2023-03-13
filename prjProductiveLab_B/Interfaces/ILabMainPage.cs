using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface ILabMainPage
    {
        Task<IEnumerable<LabMainPageDto>> GetMainPageInfo();
    }
}
