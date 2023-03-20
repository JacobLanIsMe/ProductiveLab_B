using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class ObservationNote
    {
        public int SqlId { get; set; }
        public Guid ObservationNoteId { get; set; }
        public Guid OvumPickupDetailId { get; set; }
        public string Note { get; set; } = null!;

        public virtual OvumPickupDetail OvumPickupDetail { get; set; } = null!;
    }
}
