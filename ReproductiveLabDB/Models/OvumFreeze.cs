using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OvumFreeze
    {
        public int SqlId { get; set; }
        public Guid OvumFreezeId { get; set; }
        public DateTime FreezeTime { get; set; }
        public Guid Embryologist { get; set; }
        public int StorageUnitId { get; set; }
        public Guid MediumInUseId { get; set; }
        public string? Memo { get; set; }
        public Guid OvumPickupDetailId { get; set; }

        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual MediumInUse MediumInUse { get; set; } = null!;
        public virtual OvumPickupDetail OvumPickupDetail { get; set; } = null!;
        public virtual StorageUnit StorageUnit { get; set; } = null!;
    }
}
