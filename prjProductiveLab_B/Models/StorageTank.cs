using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class StorageTank
    {
        public StorageTank()
        {
            StorageShelves = new HashSet<StorageShelf>();
        }

        public int SqlId { get; set; }
        public string TankName { get; set; } = null!;
        public int StorageTankTypeId { get; set; }

        public virtual StorageTankType StorageTankType { get; set; } = null!;
        public virtual ICollection<StorageShelf> StorageShelves { get; set; }
    }
}
