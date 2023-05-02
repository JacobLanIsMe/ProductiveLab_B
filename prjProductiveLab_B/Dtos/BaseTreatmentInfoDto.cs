using prjProductiveLab_B.Dtos.ForTreatment;

namespace prjProductiveLab_B.Dtos
{
    public class BaseTreatmentInfoDto : BaseCustomerInfoDto
    {
        // 療程編號
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
