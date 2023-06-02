using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForOperateSperm
{
    public class BaseOperateSpermInfoDto
    {
        public string? spermSituationName { get; set; }
        public string? spermRetrievalMethodName { get; set; }
        public BaseCustomerInfoDto? spermOwner { get; set; }
    }
}
