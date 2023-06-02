using ReproductiveLab_Common.Dtos.ForOperateSperm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Interfaces
{
    public interface IOperateSpermFunction
    {
        string AddSpermFreezeValidation(AddSpermFreezeDto input);
    }
}
