namespace prjProductiveLab_B.Dtos.ForTreatmentSummary
{
    public class AddFertilisationDto
    {
        public DateTime fertilisationTime { get; set; }
        public int fertilisationMethodId { get; set; }
        public int incubatorId { get; set; }
        public string? otherIncubator { get; set; }
        public List<Guid>? mediumInUseIds { get; set; }
        public Guid embryologist { get; set; }
        public List<Guid>? ovumPickupDetailIds { get; set; }
    }
}
