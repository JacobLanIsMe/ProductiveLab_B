using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class Incubator
    {
        public Incubator()
        {
            OvumPickupDetails = new HashSet<OvumPickupDetail>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<OvumPickupDetail> OvumPickupDetails { get; set; }
    }
}
