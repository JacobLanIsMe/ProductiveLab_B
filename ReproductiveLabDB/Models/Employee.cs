using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class Employee
    {
        public Employee()
        {
            CourseOfTreatments = new HashSet<CourseOfTreatment>();
            ObservationNotes = new HashSet<ObservationNote>();
            OvumFreezes = new HashSet<OvumFreeze>();
            OvumPickups = new HashSet<OvumPickup>();
            SpermFreezeSituations = new HashSet<SpermFreezeSituation>();
            SpermScores = new HashSet<SpermScore>();
        }

        public int SqlId { get; set; }
        public Guid EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public int JobTitleId { get; set; }
        public int GenderId { get; set; }
        public int IdentityServerId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual IdentityServer IdentityServer { get; set; } = null!;
        public virtual JobTitle JobTitle { get; set; } = null!;
        public virtual ICollection<CourseOfTreatment> CourseOfTreatments { get; set; }
        public virtual ICollection<ObservationNote> ObservationNotes { get; set; }
        public virtual ICollection<OvumFreeze> OvumFreezes { get; set; }
        public virtual ICollection<OvumPickup> OvumPickups { get; set; }
        public virtual ICollection<SpermFreezeSituation> SpermFreezeSituations { get; set; }
        public virtual ICollection<SpermScore> SpermScores { get; set; }
    }
}
