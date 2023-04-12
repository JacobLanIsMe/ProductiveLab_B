using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class EmbryoStatus
    {
        public EmbryoStatus()
        {
            ObservationNoteEmbryoStatuses = new HashSet<ObservationNoteEmbryoStatus>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ObservationNoteEmbryoStatus> ObservationNoteEmbryoStatuses { get; set; }
    }
}
