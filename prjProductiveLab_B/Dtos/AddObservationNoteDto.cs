namespace prjProductiveLab_B.Dtos
{
    public class AddObservationNoteDto : CommonObservationNote
    {
        public Guid embryologist { get; set; }
        public string? ovumMaturationId { get; set; }
        public string? observationTypeId { get; set; }
        public string? ovumAbnormalityId { get; set; }
        public string? fertilisationResultId { get; set; }
        public string? blastomereScore_C_Id { get; set; }
        public string? blastomereScore_G_Id { get; set; }
        public string? blastomereScore_F_Id { get; set; }
        public string? embryoStatusId { get; set; }
        public string? blastocystScore_Expansion_Id { get; set; }
        public string? blastocystScore_ICE_Id { get; set; }
        public string? blastocystScore_TE_Id { get; set; }
        public string? operationTypeId { get; set; }
        public string? mainPhotoIndex { get; set; }
        public List<IFormFile>? photos { get; set; }
    }
}
