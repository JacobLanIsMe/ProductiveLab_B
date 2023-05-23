using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Services
{
    public class FunctionService : IFunctionService
    {
        private readonly IFunctionRepository _functionRepository;
        public FunctionService(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }
        public List<FunctionDto> GetAllFunctions()
        {
            return _functionRepository.GetAllFunctions();
        }
        public List<FunctionDto> GetSubfunctions(int functionId)
        {
            return _functionRepository.GetSubfunctions(functionId);
        }
    }
}
