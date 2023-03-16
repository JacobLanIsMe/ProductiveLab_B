using prjProductiveLab_B.Models;

namespace prjProductiveLab_B.Dtos
{
    public class AddMediumDto
    {
        public string? name { get; set; }
        public DateTime? openDate { get; set; }
        public DateTime? expirationDate { get; set; }
        public string? lotNumber { get; set; }
    }
}
