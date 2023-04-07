using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class Treatment
    {
        public Treatment()
        {
            CourseOfTreatments = new HashSet<CourseOfTreatment>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CourseOfTreatment> CourseOfTreatments { get; set; }
    }
}
