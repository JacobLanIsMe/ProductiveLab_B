using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class SpermPickup
    {
        public SpermPickup()
        {
            SpermFreezes = new HashSet<SpermFreeze>();
        }

        public int SqlId { get; set; }
        public Guid SpermPickupId { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public int SpermRetrievalMethodId { get; set; }

        public virtual CourseOfTreatment CourseOfTreatment { get; set; } = null!;
        public virtual SpermRetrievalMethod SpermRetrievalMethod { get; set; } = null!;
        public virtual ICollection<SpermFreeze> SpermFreezes { get; set; }
    }
}
