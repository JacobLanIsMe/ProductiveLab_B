using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForTreatment
{
    public class AddFertilizationDto
    {
        public DateTime fertilizationTime { get; set; }
        public int fertilizationMethodId { get; set; }
        public int incubatorId { get; set; }
        public string? otherIncubator { get; set; }
        public List<Guid>? mediumInUseIds { get; set; }
        public Guid embryologist { get; set; }
        public List<Guid> ovumDetailIds { get; set; }
    }
}
