using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class StorageTank
    {
        public StorageTank()
        {
            StorageCanists = new HashSet<StorageCanist>();
        }

        public int SqlId { get; set; }
        public string TankName { get; set; } = null!;
        public int StorageTankTypeId { get; set; }

        public virtual StorageTankType StorageTankType { get; set; } = null!;
        public virtual ICollection<StorageCanist> StorageCanists { get; set; }
    }
}
