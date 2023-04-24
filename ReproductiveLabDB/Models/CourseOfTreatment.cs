using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class CourseOfTreatment
    {
        public CourseOfTreatment()
        {
            InverseOvumFromCourseOfTreatment = new HashSet<CourseOfTreatment>();
            InverseSpermFromCourseOfTreatment = new HashSet<CourseOfTreatment>();
            OvumPickups = new HashSet<OvumPickup>();
            OvumThaws = new HashSet<OvumThaw>();
            SpermFreezes = new HashSet<SpermFreeze>();
            SpermScores = new HashSet<SpermScore>();
            SpermThaws = new HashSet<SpermThaw>();
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
        public virtual CourseOfTreatment? OvumFromCourseOfTreatment { get; set; }
        public virtual CourseOfTreatment? SpermFromCourseOfTreatment { get; set; }
        public virtual Treatment Treatment { get; set; } = null!;
        public virtual TreatmentStatus TreatmentStatus { get; set; } = null!;
        public virtual ICollection<CourseOfTreatment> InverseOvumFromCourseOfTreatment { get; set; }
        public virtual ICollection<CourseOfTreatment> InverseSpermFromCourseOfTreatment { get; set; }
        public virtual ICollection<OvumPickup> OvumPickups { get; set; }
        public virtual ICollection<OvumThaw> OvumThaws { get; set; }
        public virtual ICollection<SpermFreeze> SpermFreezes { get; set; }
        public virtual ICollection<SpermScore> SpermScores { get; set; }
        public virtual ICollection<SpermThaw> SpermThaws { get; set; }
    }
}
