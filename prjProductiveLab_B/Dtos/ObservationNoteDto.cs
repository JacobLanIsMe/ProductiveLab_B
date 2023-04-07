namespace prjProductiveLab_B.Dtos
{
    public class ObservationNoteDto
    {
        public Guid ovumPickupDetailId { get; set; }
        public DateTime? ovumPickupDate { get; set; }
        public int ovumNumber { get; set; }
        public List<Observation> observationNote { get; set; }
        public List<List<Observation>> orderedObservationNote { get; set; } = new List<List<Observation>>();
    }

    public class Observation
    {
        public Guid observationNoteId { get; set; }
        public string? observationType { get; set; }
        public int?  day { get; set; }
        public DateTime? observationTime { get; set; }
        public string? mainPhoto { get; set; }
        public string? mainPhotoBase64 { get; set; }
        
    }
}
