using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface IFunctionService
    {
        Task<IEnumerable<FunctionDto>> GetCommonFunctions();
        Task<IEnumerable<FunctionDto>> GetCaseSpecificFunctions();
    }
}
