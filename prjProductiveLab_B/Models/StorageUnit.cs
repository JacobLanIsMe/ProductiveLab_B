using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class StorageUnit
    {
        public StorageUnit()
        {
            SpermFreezes = new HashSet<SpermFreeze>();
        }

        public int SqlId { get; set; }
        public string UnitName { get; set; } = null!;
        public int StorageCaneBoxId { get; set; }
        public bool IsOccupied { get; set; }

        public virtual StorageCaneBox StorageCaneBox { get; set; } = null!;
        public virtual ICollection<SpermFreeze> SpermFreezes { get; set; }
    }
}
