using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class ObservationNoteOvumAbnormality
    {
        public int SqlId { get; set; }
        public Guid Id { get; set; }
        public Guid ObservationNoteId { get; set; }
        public int OvumAbnormalityId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ObservationNote ObservationNote { get; set; } = null!;
        public virtual OvumAbnormality OvumAbnormality { get; set; } = null!;
    }
}
