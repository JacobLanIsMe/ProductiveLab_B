using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class StorageShelf
    {
        public StorageShelf()
        {
            StorageCaneBoxes = new HashSet<StorageCaneBox>();
        }

        public int SqlId { get; set; }
        public string ShelfName { get; set; } = null!;
        public int StorageTankId { get; set; }

        public virtual StorageTank StorageTank { get; set; } = null!;
        public virtual ICollection<StorageCaneBox> StorageCaneBoxes { get; set; }
    }
}
