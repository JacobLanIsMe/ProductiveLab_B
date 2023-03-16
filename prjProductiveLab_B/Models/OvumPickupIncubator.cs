using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class OvumPickupIncubator
    {
        public int SqlId { get; set; }
        public Guid OvumPickupIncubatorId { get; set; }
        public Guid OvumPickupId { get; set; }
        public int OvumNumberFrom { get; set; }
        public int OvumNumberTo { get; set; }
        public int IncubatorId { get; set; }

        public virtual Incubator Incubator { get; set; } = null!;
        public virtual OvumPickup OvumPickup { get; set; } = null!;
    }
}
