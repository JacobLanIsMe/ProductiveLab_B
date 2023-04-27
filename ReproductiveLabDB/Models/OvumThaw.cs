using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OvumThaw
    {
        public OvumThaw()
        {
            OvumPickupDetails = new HashSet<OvumPickupDetail>();
            OvumThawFreezePairs = new HashSet<OvumThawFreezePair>();
        }

        public int SqlId { get; set; }
        public Guid OvumThawId { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public DateTime ThawTime { get; set; }

        public virtual CourseOfTreatment CourseOfTreatment { get; set; } = null!;
        public virtual ICollection<OvumPickupDetail> OvumPickupDetails { get; set; }
        public virtual ICollection<OvumThawFreezePair> OvumThawFreezePairs { get; set; }
    }
}
