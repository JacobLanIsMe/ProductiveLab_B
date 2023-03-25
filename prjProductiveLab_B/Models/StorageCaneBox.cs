using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class StorageCaneBox
    {
        public StorageCaneBox()
        {
            StorageUnits = new HashSet<StorageUnit>();
        }

        public int SqlId { get; set; }
        public string CaneBoxName { get; set; } = null!;
        public int StorageShelfId { get; set; }

        public virtual StorageShelf StorageShelf { get; set; } = null!;
        public virtual ICollection<StorageUnit> StorageUnits { get; set; }
    }
}
