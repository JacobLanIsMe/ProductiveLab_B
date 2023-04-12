using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class StorageUnit
    {
        public StorageUnit()
        {
            OvumFreezes = new HashSet<OvumFreeze>();
            SpermFreezes = new HashSet<SpermFreeze>();
        }

        public int SqlId { get; set; }
        public string UnitName { get; set; } = null!;
        public int StorageCaneBoxId { get; set; }
        public bool IsOccupied { get; set; }

        public virtual StorageCaneBox StorageCaneBox { get; set; } = null!;
        public virtual ICollection<OvumFreeze> OvumFreezes { get; set; }
        public virtual ICollection<SpermFreeze> SpermFreezes { get; set; }
    }
}
