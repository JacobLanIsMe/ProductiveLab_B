namespace prjProductiveLab_B.Dtos
{
    public class BaseCustomerInfoDto
    {
        // 客戶病歷號
        public int customerSqlId { get; set; }
        // 客戶姓名
        public string? customerName { get; set; }
        // 客戶出生年月日
        public DateTime? birthday { get; set; }
    }
}
