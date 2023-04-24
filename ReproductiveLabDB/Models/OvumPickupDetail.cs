using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OvumPickupDetail
    {
        public OvumPickupDetail()
        {
            ObservationNotes = new HashSet<ObservationNote>();
            OvumThawFreezePairFreezeOvumPickupDetails = new HashSet<OvumThawFreezePair>();
            OvumThawFreezePairThawOvumPickupDetails = new HashSet<OvumThawFreezePair>();
        }

        public int SqlId { get; set; }
        public Guid OvumPickupDetailId { get; set; }
        public Guid? OvumPickupId { get; set; }
        public int OvumNumber { get; set; }
        public int? IncubatorId { get; set; }
        public Guid? MediumInUseId { get; set; }
        public int OvumPickupDetailStatusId { get; set; }
        public int FertilisationStatusId { get; set; }
        public Guid? OvumFreezeId { get; set; }
        public Guid? OvumThawId { get; set; }

        public virtual FertilisationStatus FertilisationStatus { get; set; } = null!;
        public virtual Incubator? Incubator { get; set; }
        public virtual MediumInUse? MediumInUse { get; set; }
        public virtual OvumFreeze? OvumFreeze { get; set; }
        public virtual OvumPickup? OvumPickup { get; set; }
        public virtual OvumPickupDetailStatus OvumPickupDetailStatus { get; set; } = null!;
        public virtual OvumThaw? OvumThaw { get; set; }
        public virtual ICollection<ObservationNote> ObservationNotes { get; set; }
        public virtual ICollection<OvumThawFreezePair> OvumThawFreezePairFreezeOvumPickupDetails { get; set; }
        public virtual ICollection<OvumThawFreezePair> OvumThawFreezePairThawOvumPickupDetails { get; set; }
    }
}
