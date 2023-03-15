using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface IFunctionService
    {
        Task<List<FunctionDto>> GetCommonFunctions();
        Task<List<FunctionDto>> GetCaseSpecificFunctions();
    }
}
