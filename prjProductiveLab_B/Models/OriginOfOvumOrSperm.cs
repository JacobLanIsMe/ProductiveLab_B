using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class OriginOfOvumOrSperm
    {
        public OriginOfOvumOrSperm()
        {
            TreatmentOvumFromNavigations = new HashSet<Treatment>();
            TreatmentSpermFromNavigations = new HashSet<Treatment>();
        }

        public int SqlId { get; set; }
        public string Origin { get; set; } = null!;

        public virtual ICollection<Treatment> TreatmentOvumFromNavigations { get; set; }
        public virtual ICollection<Treatment> TreatmentSpermFromNavigations { get; set; }
    }
}
