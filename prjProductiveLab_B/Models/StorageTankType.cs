﻿using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class StorageTankType
    {
        public StorageTankType()
        {
            StorageTanks = new HashSet<StorageTank>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<StorageTank> StorageTanks { get; set; }
    }
}
