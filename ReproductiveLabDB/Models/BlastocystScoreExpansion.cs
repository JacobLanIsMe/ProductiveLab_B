using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class BlastocystScoreExpansion
    {
        public BlastocystScoreExpansion()
        {
            ObservationNotes = new HashSet<ObservationNote>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ObservationNote> ObservationNotes { get; set; }
    }
}
