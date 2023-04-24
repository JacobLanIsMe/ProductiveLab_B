using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OvumThawFreezePair
    {
        public int SqlId { get; set; }
        public Guid OvumThawFreezePairId { get; set; }
        public Guid OvumThawId { get; set; }
        public Guid FreezeOvumPickupDetailId { get; set; }
        public Guid ThawOvumPickupDetailId { get; set; }

        public virtual OvumPickupDetail FreezeOvumPickupDetail { get; set; } = null!;
        public virtual OvumThaw OvumThaw { get; set; } = null!;
        public virtual OvumPickupDetail ThawOvumPickupDetail { get; set; } = null!;
    }
}
