namespace prjProductiveLab_B.Dtos.ForFreezeSummary
{
    public class GetSpermFreezeSummaryDto
    {
        public string? spermSource { get; set; }
        public int courseOfTreatmentSqlId { get; set; }
        public string? spermSituation { get; set; }
        public DateTime surgicalTime { get; set; }
        public DateTime freezeTime { get; set; }
        public int vialNumber { get; set; }
        public string? tankName { get; set; }
        public string? canistName { get; set; }
        public int boxId { get; set; }
        public string? unitName { get; set; }
        public string? freezeMediumName { get; set; }
    }
}
