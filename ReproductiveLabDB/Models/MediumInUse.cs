using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class MediumInUse
    {
        public MediumInUse()
        {
            OvumFreezes = new HashSet<OvumFreeze>();
            OvumPickupDetails = new HashSet<OvumPickupDetail>();
            SpermFreezeFreezeMediumInUses = new HashSet<SpermFreeze>();
            SpermFreezeMediumInUseId1Navigations = new HashSet<SpermFreeze>();
            SpermFreezeMediumInUseId2Navigations = new HashSet<SpermFreeze>();
            SpermFreezeMediumInUseId3Navigations = new HashSet<SpermFreeze>();
        }

        public int SqlId { get; set; }
        public Guid MediumInUseId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime OpenDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string LotNumber { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int? MediumTypeId { get; set; }

        public virtual MediumType? MediumType { get; set; }
        public virtual ICollection<OvumFreeze> OvumFreezes { get; set; }
        public virtual ICollection<OvumPickupDetail> OvumPickupDetails { get; set; }
        public virtual ICollection<SpermFreeze> SpermFreezeFreezeMediumInUses { get; set; }
        public virtual ICollection<SpermFreeze> SpermFreezeMediumInUseId1Navigations { get; set; }
        public virtual ICollection<SpermFreeze> SpermFreezeMediumInUseId2Navigations { get; set; }
        public virtual ICollection<SpermFreeze> SpermFreezeMediumInUseId3Navigations { get; set; }
    }
}
