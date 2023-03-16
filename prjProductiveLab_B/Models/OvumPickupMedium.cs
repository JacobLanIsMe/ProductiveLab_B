using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class OvumPickupMedium
    {
        public int SqlId { get; set; }
        public Guid OvumPickupMediumId { get; set; }
        public Guid OvumPickupId { get; set; }
        public Guid MediumInUseId { get; set; }

        public virtual MediumInUse MediumInUse { get; set; } = null!;
        public virtual OvumPickup OvumPickup { get; set; } = null!;
    }
}
