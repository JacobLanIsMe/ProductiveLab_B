using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForTreatment
{
    public class BaseTreatmentInfoDto : BaseCustomerInfoDto
    {
        public int courseOfTreatmentSqlId { get; set; }

        // 客戶配偶病歷號
        public int? spouseSqlId { get; set; }
        // 客戶配偶姓名
        public string? spouseName { get; set; }
        // 主治醫師
        public string? doctor { get; set; }
        // 療程名稱
        public TreatmentDto? treatment { get; set; }
        public string? memo { get; set; }
    }
}
