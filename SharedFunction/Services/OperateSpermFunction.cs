using Reproductive_SharedFunction.Interfaces;
using ReproductiveLab_Common.Dtos.ForOperateSperm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Services
{
    public class OperateSpermFunction : IOperateSpermFunction
    {
        public string AddSpermFreezeValidation(AddSpermFreezeDto input)
        {
            string errorMessage = "";
            if (input.mediumInUseArray == null || input.mediumInUseArray.Count == 0 || input.mediumInUseArray.Count > 3)
            {
                errorMessage += "培養液資訊有誤\n";
            }
            if (input.storageUnitId == null || input.storageUnitId.Count == 0)
            {
                errorMessage += "請選擇儲位\n";
            }
            return errorMessage;
        }
    }
}
