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
        public Guid OvumPickupDetailId1 { get; set; }
        public string? OtherMediumName { get; set; }
        public Guid? OvumPickupDetailId2 { get; set; }
        public Guid? OvumPickupDetailId3 { get; set; }
        public Guid? OvumPickupDetailId4 { get; set; }
        public int OvumMorphologyA { get; set; }
        public int OvumMorphologyB { get; set; }
        public int OvumMorphologyC { get; set; }

        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual MediumInUse MediumInUse { get; set; } = null!;
        public virtual OvumPickupDetail OvumPickupDetailId1Navigation { get; set; } = null!;
        public virtual OvumPickupDetail? OvumPickupDetailId2Navigation { get; set; }
        public virtual OvumPickupDetail? OvumPickupDetailId3Navigation { get; set; }
        public virtual OvumPickupDetail? OvumPickupDetailId4Navigation { get; set; }
        public virtual StorageUnit StorageUnit { get; set; } = null!;
    }
}
