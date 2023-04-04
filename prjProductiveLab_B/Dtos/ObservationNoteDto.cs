namespace prjProductiveLab_B.Dtos
{
    public class ObservationNoteDto
    {
        public DateTime? ovumPickupDate { get; set; }
        public int ovumNumber { get; set; }
        public List<Observation> observationNote { get; set; }
        public List<Observation> orderedObservationNote { get; set; } = new List<Observation>();
    }
    public class Observation
    {
        public string? observationType { get; set; }
        public int?  day { get; set; }
        public DateTime? observationTime { get; set; }
        public string? mainPhoto { get; set; }
    }
}
