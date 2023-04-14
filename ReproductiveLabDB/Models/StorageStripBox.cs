using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class StorageStripBox
    {
        public StorageStripBox()
        {
            StorageUnits = new HashSet<StorageUnit>();
        }

        public int SqlId { get; set; }
        public string StripBoxName { get; set; } = null!;
        public int StorageCanistId { get; set; }

        public virtual StorageCanist StorageCanist { get; set; } = null!;
        public virtual ICollection<StorageUnit> StorageUnits { get; set; }
    }
}
