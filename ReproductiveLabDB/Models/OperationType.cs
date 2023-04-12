using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OperationType
    {
        public OperationType()
        {
            ObservationNoteOperations = new HashSet<ObservationNoteOperation>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ObservationNoteOperation> ObservationNoteOperations { get; set; }
    }
}
