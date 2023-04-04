﻿using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class BlastomereScoreC
    {
        public BlastomereScoreC()
        {
            ObservationNotes = new HashSet<ObservationNote>();
        }

        public int SlqId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ObservationNote> ObservationNotes { get; set; }
    }
}
