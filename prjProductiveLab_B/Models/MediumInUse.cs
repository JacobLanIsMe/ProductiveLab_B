using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class MediumInUse
    {
        public MediumInUse()
        {
            OvumPickupDetails = new HashSet<OvumPickupDetail>();
            SpermFreezeFreezeMediumInUses = new HashSet<SpermFreeze>();
            SpermFreezeMediumInUses = new HashSet<SpermFreeze>();
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
        public virtual ICollection<OvumPickupDetail> OvumPickupDetails { get; set; }
        public virtual ICollection<SpermFreeze> SpermFreezeFreezeMediumInUses { get; set; }
        public virtual ICollection<SpermFreeze> SpermFreezeMediumInUses { get; set; }
    }
}
