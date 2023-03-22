using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class SpermScore
    {
        public SpermScore()
        {
            SpermFreezes = new HashSet<SpermFreeze>();
        }

        public int SqlId { get; set; }
        public Guid SpermScoreId { get; set; }
        public decimal Volume { get; set; }
        public decimal Concentration { get; set; }
        public decimal ActivityA { get; set; }
        public decimal ActivityB { get; set; }
        public decimal ActivityC { get; set; }
        public decimal ActivityD { get; set; }
        public decimal? Morphology { get; set; }
        public int? Abstinence { get; set; }
        public int SpermScoreTimePointId { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public DateTime RecordTime { get; set; }
        public Guid Embryologist { get; set; }

        public virtual CourseOfTreatment CourseOfTreatment { get; set; } = null!;
        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual SpermScoreTimePoint SpermScoreTimePoint { get; set; } = null!;
        public virtual ICollection<SpermFreeze> SpermFreezes { get; set; }
    }
}
