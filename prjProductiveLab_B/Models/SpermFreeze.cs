using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class SpermFreeze
    {
        public int SqlId { get; set; }
        public Guid SpermFreezeId { get; set; }
        public int VialNumber { get; set; }
        public Guid SpermScoreId { get; set; }

        public virtual SpermScore SpermScore { get; set; } = null!;
    }
}
