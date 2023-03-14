using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class Employee
    {
        public Employee()
        {
            CourseOfTreatmentDoctorNavigations = new HashSet<CourseOfTreatment>();
            CourseOfTreatmentEmbryologistNavigations = new HashSet<CourseOfTreatment>();
        }

        public int SqlId { get; set; }
        public Guid StaffId { get; set; }
        public string Name { get; set; } = null!;
        public int JobTitleId { get; set; }
        public int GenderId { get; set; }
        public int IdentityServerId { get; set; }

        public virtual IdentityServer IdentityServer { get; set; } = null!;
        public virtual JobTitle JobTitle { get; set; } = null!;
        public virtual ICollection<CourseOfTreatment> CourseOfTreatmentDoctorNavigations { get; set; }
        public virtual ICollection<CourseOfTreatment> CourseOfTreatmentEmbryologistNavigations { get; set; }
    }
}
