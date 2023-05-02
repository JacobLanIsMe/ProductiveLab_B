using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OvumThawFreezePair
    {
        public int SqlId { get; set; }
        public Guid OvumThawFreezePairId { get; set; }
        public Guid FreezeOvumDetailId { get; set; }
        public Guid ThawOvumDetailId { get; set; }

        public virtual OvumDetail FreezeOvumDetail { get; set; } = null!;
        public virtual OvumDetail ThawOvumDetail { get; set; } = null!;
    }
}
