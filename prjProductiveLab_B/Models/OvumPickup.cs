using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class OvumPickup
    {
        public OvumPickup()
        {
            OvumPickupIncubators = new HashSet<OvumPickupIncubator>();
            OvumPickupMedia = new HashSet<OvumPickupMedium>();
        }

        public int SqlId { get; set; }
        public Guid OvumPickupId { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public DateTime TriggerTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalOvumNumber { get; set; }
        public int CocGrade5 { get; set; }
        public int CocGrade4 { get; set; }
        public int CocGrade3 { get; set; }
        public int CocGrade2 { get; set; }
        public int CocGrade1 { get; set; }
        public Guid Embryologist { get; set; }

        public virtual CourseOfTreatment CourseOfTreatment { get; set; } = null!;
        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual ICollection<OvumPickupIncubator> OvumPickupIncubators { get; set; }
        public virtual ICollection<OvumPickupMedium> OvumPickupMedia { get; set; }
    }
}
