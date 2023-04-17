using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class TopColor
    {
        public TopColor()
        {
            OvumFreezes = new HashSet<OvumFreeze>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<OvumFreeze> OvumFreezes { get; set; }
    }
}
