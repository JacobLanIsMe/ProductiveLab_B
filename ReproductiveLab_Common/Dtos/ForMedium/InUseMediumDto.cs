using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForMedium
{
    public class InUseMediumDto
    {
        public string? mediumInUseId { get; set; }
        public string? name { get; set; }
        public DateTime? openDate { get; set; }
        public DateTime? expirationDate { get; set; }
        public string? lotNumber { get; set; }
        public bool? isDeleted { get; set; }
        public int? mediumTypeId { get; set; }
    }
}
