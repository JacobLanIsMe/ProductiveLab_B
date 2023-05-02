using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class ObservationNoteEmbryoStatus
    {
        public int SqlId { get; set; }
        public Guid Id { get; set; }
        public Guid ObservationNoteId { get; set; }
        public int EmbryoStatusId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual EmbryoStatus EmbryoStatus { get; set; } = null!;
        public virtual ObservationNote ObservationNote { get; set; } = null!;
    }
}
