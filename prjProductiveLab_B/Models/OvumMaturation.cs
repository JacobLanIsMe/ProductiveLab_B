using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class OvumMaturation
    {
        public OvumMaturation()
        {
            ObservationNotes = new HashSet<ObservationNote>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ObservationNote> ObservationNotes { get; set; }
    }
}
