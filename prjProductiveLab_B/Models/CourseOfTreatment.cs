using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class CourseOfTreatment
    {
        public CourseOfTreatment()
        {
            OvumPickups = new HashSet<OvumPickup>();
            SpermPickups = new HashSet<SpermPickup>();
        }

        public int SqlId { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public Guid Doctor { get; set; }
        public Guid CustomerId { get; set; }
        public int TreatmentId { get; set; }
        public DateTime SurgicalTime { get; set; }
        public int TreatmentStatusId { get; set; }
        public string? Memo { get; set; }
        public Guid? OvumFromCourseOfTreatmentId { get; set; }
        public Guid? SpermFromCourseOfTreatmentId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Employee DoctorNavigation { get; set; } = null!;
        public virtual Treatment Treatment { get; set; } = null!;
        public virtual TreatmentStatus TreatmentStatus { get; set; } = null!;
        public virtual ICollection<OvumPickup> OvumPickups { get; set; }
        public virtual ICollection<SpermPickup> SpermPickups { get; set; }
    }
}
