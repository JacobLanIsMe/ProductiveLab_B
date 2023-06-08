using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class ObservationNoteOperation
    {
        public int SqlId { get; set; }
        public byte[] Id { get; set; } = null!;
        public Guid ObservationNoteId { get; set; }
        public int OperationTypeId { get; set; }
        public string? SpindleResult { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ObservationNote ObservationNote { get; set; } = null!;
        public virtual OperationType OperationType { get; set; } = null!;
    }
}
