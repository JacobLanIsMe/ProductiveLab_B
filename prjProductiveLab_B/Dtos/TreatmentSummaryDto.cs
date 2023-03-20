namespace prjProductiveLab_B.Dtos
{
    public class TreatmentSummaryDto
    {
        public Guid? ovumPickupDetailId { get; set; }
       public string? originOfOvum { get; set; }
        public int ovumFromCourseOfTreatmentSqlId { get; set; }
        public string? ovumPickupDetailStatus { get; set; }
        public int dateOfEmbryo { get; set; }
        public int ovumNumber { get; set; }
        public string? fertilizationStatus { get; set; }
        public string? observationNote { get; set; }
    }
}
