﻿using prjProductiveLab_B.Dtos;

namespace prjProductiveLab_B.Interfaces
{
    public interface IFunctionService
    {
        Task<List<FunctionDto>> GetAllFunctions();
        Task<List<FunctionDto>> GetSubfunctions(int functionId);
    }
}
