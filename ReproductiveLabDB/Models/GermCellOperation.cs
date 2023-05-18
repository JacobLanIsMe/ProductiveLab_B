using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class GermCellOperation
    {
        public GermCellOperation()
        {
            CourseOfTreatmentEmbryoOperations = new HashSet<CourseOfTreatment>();
            CourseOfTreatmentOvumOperations = new HashSet<CourseOfTreatment>();
            CourseOfTreatmentSpermOperations = new HashSet<CourseOfTreatment>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CourseOfTreatment> CourseOfTreatmentEmbryoOperations { get; set; }
        public virtual ICollection<CourseOfTreatment> CourseOfTreatmentOvumOperations { get; set; }
        public virtual ICollection<CourseOfTreatment> CourseOfTreatmentSpermOperations { get; set; }
    }
}
