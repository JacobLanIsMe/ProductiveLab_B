using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class Employee
    {
        public Employee()
        {
            CourseOfTreatments = new HashSet<CourseOfTreatment>();
            Fertilizations = new HashSet<Fertilization>();
            ObservationNotes = new HashSet<ObservationNote>();
            OvumFreezes = new HashSet<OvumFreeze>();
            OvumPickups = new HashSet<OvumPickup>();
            OvumThawEmbryologistNavigations = new HashSet<OvumThaw>();
            OvumThawRecheckEmbryologistNavigations = new HashSet<OvumThaw>();
            SpermFreezeSituations = new HashSet<SpermFreezeSituation>();
            SpermScores = new HashSet<SpermScore>();
            SpermThawEmbryologistNavigations = new HashSet<SpermThaw>();
            SpermThawRecheckEmbryologistNavigations = new HashSet<SpermThaw>();
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
        public virtual ICollection<Fertilization> Fertilizations { get; set; }
        public virtual ICollection<ObservationNote> ObservationNotes { get; set; }
        public virtual ICollection<OvumFreeze> OvumFreezes { get; set; }
        public virtual ICollection<OvumPickup> OvumPickups { get; set; }
        public virtual ICollection<OvumThaw> OvumThawEmbryologistNavigations { get; set; }
        public virtual ICollection<OvumThaw> OvumThawRecheckEmbryologistNavigations { get; set; }
        public virtual ICollection<SpermFreezeSituation> SpermFreezeSituations { get; set; }
        public virtual ICollection<SpermScore> SpermScores { get; set; }
        public virtual ICollection<SpermThaw> SpermThawEmbryologistNavigations { get; set; }
        public virtual ICollection<SpermThaw> SpermThawRecheckEmbryologistNavigations { get; set; }
    }
}
