using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class OvumPickup
    {
        public OvumPickup()
        {
            OvumPickupDetails = new HashSet<OvumPickupDetail>();
        }

        public int SqlId { get; set; }
        public Guid OvumPickupId { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public DateTime TriggerTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CocGrade5 { get; set; }
        public int CocGrade4 { get; set; }
        public int CocGrade3 { get; set; }
        public int CocGrade2 { get; set; }
        public int CocGrade1 { get; set; }
        public Guid Embryologist { get; set; }
        public DateTime UpdateTime { get; set; }
        public Guid MediumInUseId1 { get; set; }
        public Guid? MediumInUseId2 { get; set; }
        public Guid? MediumInUseId3 { get; set; }

        public virtual CourseOfTreatment CourseOfTreatment { get; set; } = null!;
        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual ICollection<OvumPickupDetail> OvumPickupDetails { get; set; }
    }
}
