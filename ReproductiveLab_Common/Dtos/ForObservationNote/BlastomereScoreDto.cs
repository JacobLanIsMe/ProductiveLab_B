using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForObservationNote
{
    public class BlastomereScoreDto
    {
        public List<Common1Dto> blastomereScore_C { get; set; }
        public List<Common1Dto> blastomereScore_G { get; set; }
        public List<Common1Dto> blastomereScore_F { get; set; }
    }
}
