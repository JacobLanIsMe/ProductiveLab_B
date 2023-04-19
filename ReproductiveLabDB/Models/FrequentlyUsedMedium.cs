using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class FrequentlyUsedMedium
    {
        public int SqlId { get; set; }
        public string Name { get; set; } = null!;
        public int? MediumTypeId { get; set; }

        public virtual MediumType? MediumType { get; set; }
    }
}
