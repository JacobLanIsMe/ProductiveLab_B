using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class OvumPickupIncubator
    {
        public int SqlId { get; set; }
        public Guid OvumPickupIncubatorId { get; set; }
        public Guid OvumPickupId { get; set; }
        public int OvumNumber { get; set; }

        public virtual OvumPickup OvumPickup { get; set; } = null!;
    }
}
