using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OvumDetailStatus
    {
        public OvumDetailStatus()
        {
            OvumDetails = new HashSet<OvumDetail>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<OvumDetail> OvumDetails { get; set; }
    }
}
