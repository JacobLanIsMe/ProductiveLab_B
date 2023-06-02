using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForOperateSperm
{
    public class AddSpermThawDto
    {
        public Guid courseOfTreatmentId { get; set; }
        public DateTime thawTime { get; set; }
        public Guid embryologist { get; set; }
        public int spermThawMethodId { get; set; }
        public List<Guid>? spermFreezeIds { get; set; }
        public Guid recheckEmbryologist { get; set; }
        public string? otherSpermThawMethod { get; set; }
        public List<Guid>? mediumInUseIds { get; set; }
    }
}
