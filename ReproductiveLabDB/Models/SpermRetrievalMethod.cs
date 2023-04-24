using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class SpermRetrievalMethod
    {
        public SpermRetrievalMethod()
        {
            Treatments = new HashSet<Treatment>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
