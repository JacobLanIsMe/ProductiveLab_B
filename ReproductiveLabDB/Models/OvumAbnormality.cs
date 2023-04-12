using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OvumAbnormality
    {
        public OvumAbnormality()
        {
            ObservationNoteOvumAbnormalities = new HashSet<ObservationNoteOvumAbnormality>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ObservationNoteOvumAbnormality> ObservationNoteOvumAbnormalities { get; set; }
    }
}
