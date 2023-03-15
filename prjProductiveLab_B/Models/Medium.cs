﻿using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class Medium
    {
        public Medium()
        {
            OvumPickupMedia = new HashSet<OvumPickupMedium>();
        }

        public int SqlId { get; set; }
        public Guid MediumId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime OpenDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string LotNumber { get; set; } = null!;

        public virtual ICollection<OvumPickupMedium> OvumPickupMedia { get; set; }
    }
}
