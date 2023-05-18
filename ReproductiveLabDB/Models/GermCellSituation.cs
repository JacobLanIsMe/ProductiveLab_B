using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class GermCellSituation
    {
        public GermCellSituation()
        {
            CourseOfTreatmentEmbryoSituations = new HashSet<CourseOfTreatment>();
            CourseOfTreatmentOvumSituations = new HashSet<CourseOfTreatment>();
            CourseOfTreatmentSpermSituations = new HashSet<CourseOfTreatment>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CourseOfTreatment> CourseOfTreatmentEmbryoSituations { get; set; }
        public virtual ICollection<CourseOfTreatment> CourseOfTreatmentOvumSituations { get; set; }
        public virtual ICollection<CourseOfTreatment> CourseOfTreatmentSpermSituations { get; set; }
    }
}
