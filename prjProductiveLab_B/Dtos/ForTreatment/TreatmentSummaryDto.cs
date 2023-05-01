namespace prjProductiveLab_B.Dtos.ForTreatment
{
    public class TreatmentSummaryDto
    {
        public Guid? ovumPickupDetailId { get; set; }
        public int courseOfTreatmentSqlId { get; set; }
        public int? ovumFromCourseOfTreatmentSqlId { get; set; }
        public string? ovumPickupDetailStatus { get; set; }
        public int dateOfEmbryo { get; set; }
        public int ovumNumber { get; set; }
        public bool hasFertilization { get; set; }
        public string? observationNote { get; set; }
        public string? ovumSource { get; set; }
    }
}
