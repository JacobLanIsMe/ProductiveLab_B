namespace prjProductiveLab_B.Dtos
{
    public class AddCourseOfTreatmentDto
    {
        public Guid doctorId { get; set; }
        public Guid customerId { get; set; }
        public int treatmentId { get; set; }
        public DateTime surgicalTime { get; set; }
        public string? memo { get; set; }
    }
}
