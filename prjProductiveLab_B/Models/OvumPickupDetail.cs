using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class OvumPickupDetail
    {
        public int SqlId { get; set; }
        public Guid OvumPickupDetailId { get; set; }
        public Guid OvumPickupId { get; set; }
        public int OvumNumber { get; set; }
        public int IncubatorId { get; set; }
        public Guid MediumInUseId { get; set; }

        public virtual Incubator Incubator { get; set; } = null!;
        public virtual MediumInUse MediumInUse { get; set; } = null!;
        public virtual OvumPickup OvumPickup { get; set; } = null!;
    }
}
