using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class SpermFreezeOperationMethod
    {
        public SpermFreezeOperationMethod()
        {
            SpermFreezes = new HashSet<SpermFreeze>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<SpermFreeze> SpermFreezes { get; set; }
    }
}
