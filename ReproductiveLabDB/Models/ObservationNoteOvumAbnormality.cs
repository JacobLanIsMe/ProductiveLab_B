using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class ObservationNoteOvumAbnormality
    {
        public int SqlId { get; set; }
        public Guid Id { get; set; }
        public Guid ObservationNoteId { get; set; }
        public int ForeignKeyId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual OvumAbnormality ForeignKey { get; set; } = null!;
        public virtual ObservationNote ObservationNote { get; set; } = null!;
    }
}
