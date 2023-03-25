using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class StorageStrip
    {
        public StorageStrip()
        {
            StorageCaneBoxes = new HashSet<StorageCaneBox>();
        }

        public int SqlId { get; set; }
        public string StripName { get; set; } = null!;
        public int StorageTankId { get; set; }

        public virtual StorageTank StorageTank { get; set; } = null!;
        public virtual ICollection<StorageCaneBox> StorageCaneBoxes { get; set; }
    }
}
