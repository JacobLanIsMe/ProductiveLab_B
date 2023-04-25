using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class SpermThaw
    {
        public int SqlId { get; set; }
        public Guid SpermThawId { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public int SpermThawMethodId { get; set; }

        public virtual CourseOfTreatment CourseOfTreatment { get; set; } = null!;
        public virtual SpermThawMethod SpermThawMethod { get; set; } = null!;
    }
}
