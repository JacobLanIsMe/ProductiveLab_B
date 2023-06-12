using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Interfaces
{
    public interface IEmployeeService
    {
        List<Common2Dto> GetAllEmbryologist();
        List<Common2Dto> GetAllDoctor();
    }
}
