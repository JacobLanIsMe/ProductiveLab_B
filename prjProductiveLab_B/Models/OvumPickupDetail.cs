using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class OvumPickupDetail
    {
        public OvumPickupDetail()
        {
            ObservationNotes = new HashSet<ObservationNote>();
        }

        public int SqlId { get; set; }
        public Guid OvumPickupDetailId { get; set; }
        public Guid OvumPickupId { get; set; }
        public int OvumNumber { get; set; }
        public int? IncubatorId { get; set; }
        public Guid? MediumInUseId { get; set; }
        public int OvumPickupDetailStatusId { get; set; }
        public int FertilizationStatusId { get; set; }

        public virtual FertilizationStatus FertilizationStatus { get; set; } = null!;
        public virtual Incubator? Incubator { get; set; }
        public virtual MediumInUse? MediumInUse { get; set; }
        public virtual OvumPickup OvumPickup { get; set; } = null!;
        public virtual OvumPickupDetailStatus OvumPickupDetailStatus { get; set; } = null!;
        public virtual ICollection<ObservationNote> ObservationNotes { get; set; }
    }
}
