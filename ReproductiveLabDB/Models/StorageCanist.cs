using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class StorageCanist
    {
        public StorageCanist()
        {
            StorageStripBoxes = new HashSet<StorageStripBox>();
        }

        public int SqlId { get; set; }
        public string CanistName { get; set; } = null!;
        public int StorageTankId { get; set; }

        public virtual StorageTank StorageTank { get; set; } = null!;
        public virtual ICollection<StorageStripBox> StorageStripBoxes { get; set; }
    }
}
