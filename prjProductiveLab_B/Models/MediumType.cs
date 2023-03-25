﻿using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class MediumType
    {
        public MediumType()
        {
            MediumInUses = new HashSet<MediumInUse>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<MediumInUse> MediumInUses { get; set; }
    }
}
