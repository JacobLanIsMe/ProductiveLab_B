using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForTreatment
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
