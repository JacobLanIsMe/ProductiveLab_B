using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class SpermRetrievalMethod
    {
        public SpermRetrievalMethod()
        {
            SpermPickups = new HashSet<SpermPickup>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<SpermPickup> SpermPickups { get; set; }
    }
}
