namespace prjProductiveLab_B.Dtos
{
    public class AddObservationNoteDto
    {
        public Guid ovumPickupDetailId { get; set; }
        public DateTime observationTime { get; set; }
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
        public string? memo { get; set; }
        public string? kidScore { get; set; }
        public string? pgtaNumber { get; set; }
        public string? pgtaResult { get; set; }
        public string? pgtmResult { get; set; }
        public string? operationTypeId { get; set; }
        public string? mainPhotoIndex { get; set; }
        public List<IFormFile>? photos { get; set; }
        public int day { get; set; }
    }
}
