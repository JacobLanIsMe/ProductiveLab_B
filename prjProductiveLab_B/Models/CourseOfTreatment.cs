using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class CourseOfTreatment
    {
        public int SqlId { get; set; }
        public Guid CourseOfTreatment1 { get; set; }
        public Guid Doctor { get; set; }
        public Guid Embryologist { get; set; }
        public Guid CustomerId { get; set; }
        public int TreatmentId { get; set; }
        public DateTime SurgicalTime { get; set; }
        public int TreatmentStatusId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Employee DoctorNavigation { get; set; } = null!;
        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual Treatment Treatment { get; set; } = null!;
        public virtual TreatmentStatus TreatmentStatus { get; set; } = null!;
    }
}
