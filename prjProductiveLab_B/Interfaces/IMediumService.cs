﻿using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Enums;
using prjProductiveLab_B.Models;

namespace prjProductiveLab_B.Interfaces
{
    public interface IMediumService
    {
        Task<BaseResponseDto> AddMedium(MediumInUse medium);
        Task<InUseMediumDto> GetInUseMedium(MediumTypeEnum mediumType);
    }
}
