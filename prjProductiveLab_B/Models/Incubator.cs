using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class Incubator
    {
        public Incubator()
        {
            OvumPickupIncubators = new HashSet<OvumPickupIncubator>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<OvumPickupIncubator> OvumPickupIncubators { get; set; }
    }
}
