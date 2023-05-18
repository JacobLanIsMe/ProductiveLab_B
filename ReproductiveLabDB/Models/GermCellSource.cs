using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class GermCellSource
    {
        public GermCellSource()
        {
            CourseOfTreatmentOvumSources = new HashSet<CourseOfTreatment>();
            CourseOfTreatmentSpermSources = new HashSet<CourseOfTreatment>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CourseOfTreatment> CourseOfTreatmentOvumSources { get; set; }
        public virtual ICollection<CourseOfTreatment> CourseOfTreatmentSpermSources { get; set; }
    }
}
