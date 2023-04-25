using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class SpermThawMethod
    {
        public SpermThawMethod()
        {
            SpermThaws = new HashSet<SpermThaw>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<SpermThaw> SpermThaws { get; set; }
    }
}
