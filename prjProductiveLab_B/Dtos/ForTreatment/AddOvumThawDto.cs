namespace prjProductiveLab_B.Dtos.ForTreatment
{
    public class AddOvumThawDto
    {
        public Guid courseOfTreatmentId { get; set; }
        public Guid ovumFromCourseOfTreatmentId { get; set; }
        public DateTime thawTime { get; set; }
        public Guid embryologist { get; set; }
        public Guid thawMediumInUseId { get; set; }
        public Guid recheckEmbryologist { get; set; }
        public List<Guid>? mediumInUseIds { get; set; }
        public List<Guid>? freezeOvumDetailIds { get; set; }
    }
}
