﻿using ReproductiveLab_Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Interfaces
{
    public interface ITreatmentService
    {
        List<LabMainPageDto> GetMainPageInfo();
    }
}
