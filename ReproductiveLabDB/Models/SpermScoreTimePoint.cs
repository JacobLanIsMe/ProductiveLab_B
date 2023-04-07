using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class SpermScoreTimePoint
    {
        public SpermScoreTimePoint()
        {
            SpermScores = new HashSet<SpermScore>();
        }

        public int SqlId { get; set; }
        public string TimePoint { get; set; } = null!;

        public virtual ICollection<SpermScore> SpermScores { get; set; }
    }
}
