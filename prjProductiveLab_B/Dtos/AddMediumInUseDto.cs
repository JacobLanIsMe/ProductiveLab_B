namespace prjProductiveLab_B.Dtos
{
    public class AddMediumInUseDto
    {
        public int frequentlyUsedMediumId { get; set; }
        public string? customizedMedium { get; set; }
        public DateTime openDate { get; set; }
        public DateTime expirationDate { get; set; }
        public string? lotNumber { get; set; }
        public int mediumTypeId { get; set; }
    }
}
