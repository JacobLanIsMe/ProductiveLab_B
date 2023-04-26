using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class SpermThawFreezePair
    {
        public int SqlId { get; set; }
        public Guid SpermThawFreezePairId { get; set; }
        public Guid SpermThawId { get; set; }
        public Guid SpermFreezeId { get; set; }

        public virtual SpermFreeze SpermFreeze { get; set; } = null!;
        public virtual SpermThaw SpermThaw { get; set; } = null!;
    }
}
