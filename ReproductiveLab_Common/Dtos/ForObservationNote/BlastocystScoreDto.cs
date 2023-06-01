using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForObservationNote
{
    public class BlastocystScoreDto
    {
        public List<Common1Dto> blastocystScore_Expansion { get; set; }
        public List<Common1Dto> blastocystScore_ICE { get; set; }
        public List<Common1Dto> blastocystScore_TE { get; set; }
    }
}
