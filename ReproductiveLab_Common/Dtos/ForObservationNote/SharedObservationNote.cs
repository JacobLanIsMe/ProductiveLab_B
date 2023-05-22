using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForObservationNote
{
    public class SharedObservationNote
    {
        public Guid ovumDetailId { get; set; }

        public DateTime? observationTime { get; set; }
        public string? memo { get; set; }
        public string? kidScore { get; set; }
        public string? pgtaNumber { get; set; }
        public string? pgtaResult { get; set; }
        public string? pgtmResult { get; set; }
        public int day { get; set; }
        public int? ovumNumber { get; set; }
    }
}
