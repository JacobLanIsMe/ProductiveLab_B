using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class GermCellSource
    {
        public GermCellSource()
        {
            TreatmentOvumSources = new HashSet<Treatment>();
            TreatmentSpermSources = new HashSet<Treatment>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Treatment> TreatmentOvumSources { get; set; }
        public virtual ICollection<Treatment> TreatmentSpermSources { get; set; }
    }
}
