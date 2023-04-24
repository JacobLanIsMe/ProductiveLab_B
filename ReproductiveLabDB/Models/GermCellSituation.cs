using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class GermCellSituation
    {
        public GermCellSituation()
        {
            TreatmentEmbryoSituations = new HashSet<Treatment>();
            TreatmentOvumSituations = new HashSet<Treatment>();
            TreatmentSpermSituations = new HashSet<Treatment>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Treatment> TreatmentEmbryoSituations { get; set; }
        public virtual ICollection<Treatment> TreatmentOvumSituations { get; set; }
        public virtual ICollection<Treatment> TreatmentSpermSituations { get; set; }
    }
}
