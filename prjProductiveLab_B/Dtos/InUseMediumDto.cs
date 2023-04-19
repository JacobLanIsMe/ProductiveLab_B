namespace prjProductiveLab_B.Dtos
{
    public class InUseMediumDto
    {
        public string? mediumInUseId { get; set; }
        public string? name { get; set; }
        public DateTime? openDate { get; set; }
        public DateTime? expirationDate { get; set; }
        public string? lotNumber { get; set; }
        public Boolean? isDeleted { get; set; }
        public int? mediumTypeId { get; set; }
    }
}
