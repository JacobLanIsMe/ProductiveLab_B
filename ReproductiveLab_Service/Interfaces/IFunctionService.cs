using ReproductiveLab_Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Interfaces
{
    public interface IFunctionService
    {
        List<FunctionDto> GetAllFunctions();
        List<FunctionDto> GetSubfunctions(int functionId);
    }
}
