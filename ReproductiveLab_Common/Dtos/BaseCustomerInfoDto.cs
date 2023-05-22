using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos
{
    public class BaseCustomerInfoDto
    {
        public int customerSqlId { get; set; }
        // 客戶姓名
        public string? customerName { get; set; }
        // 客戶出生年月日
        public DateTime? birthday { get; set; }
        public Guid customerId { get; set; }
    }
}
