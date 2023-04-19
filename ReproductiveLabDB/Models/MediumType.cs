using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class MediumType
    {
        public MediumType()
        {
            FrequentlyUsedMedia = new HashSet<FrequentlyUsedMedium>();
            MediumInUses = new HashSet<MediumInUse>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<FrequentlyUsedMedium> FrequentlyUsedMedia { get; set; }
        public virtual ICollection<MediumInUse> MediumInUses { get; set; }
    }
}
