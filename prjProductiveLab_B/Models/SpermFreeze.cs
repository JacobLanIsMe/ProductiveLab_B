using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class SpermFreeze
    {
        public int SqlId { get; set; }
        public Guid SpermFreezeId { get; set; }
        public Guid SpermPickupId { get; set; }
        public int VialNumber { get; set; }

        public virtual SpermPickup SpermPickup { get; set; } = null!;
    }
}
