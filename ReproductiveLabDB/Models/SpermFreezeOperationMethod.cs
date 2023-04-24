using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class SpermFreezeOperationMethod
    {
        public SpermFreezeOperationMethod()
        {
            SpermFreezeSituations = new HashSet<SpermFreezeSituation>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<SpermFreezeSituation> SpermFreezeSituations { get; set; }
    }
}
