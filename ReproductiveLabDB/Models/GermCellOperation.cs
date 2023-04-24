using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class GermCellOperation
    {
        public GermCellOperation()
        {
            TreatmentEmbryoOperations = new HashSet<Treatment>();
            TreatmentOvumOperations = new HashSet<Treatment>();
            TreatmentSpermOperations = new HashSet<Treatment>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Treatment> TreatmentEmbryoOperations { get; set; }
        public virtual ICollection<Treatment> TreatmentOvumOperations { get; set; }
        public virtual ICollection<Treatment> TreatmentSpermOperations { get; set; }
    }
}
