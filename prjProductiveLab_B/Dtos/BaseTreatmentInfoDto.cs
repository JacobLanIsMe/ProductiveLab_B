namespace prjProductiveLab_B.Dtos
{
    public class BaseTreatmentInfoDto
    {
        // 療程編號
        public int courseOfTreatmentSqlId { get; set; }
        // 客戶病歷號
        public int customerSqlId { get; set; }
        // 客戶姓名
        public string? customerName { get; set; }
        // 客戶出生年月日
        public DateTime? birthday { get; set; }
        // 客戶配偶病歷號
        public int spouseSqlId { get; set; }
        // 客戶配偶姓名
        public string? spouseName { get; set; }
        // 主治醫師
        public string? doctor { get; set; }
        // 療程名稱
        public string? treatmentName { get; set; }
        public string? memo { get; set; }
    }
}
