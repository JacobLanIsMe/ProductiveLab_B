using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OvumDetail
    {
        public OvumDetail()
        {
            ObservationNotes = new HashSet<ObservationNote>();
            OvumThawFreezePairFreezeOvumDetails = new HashSet<OvumThawFreezePair>();
            OvumThawFreezePairThawOvumDetails = new HashSet<OvumThawFreezePair>();
            OvumTransferPairDonorOvumDetails = new HashSet<OvumTransferPair>();
            OvumTransferPairRecipientOvumDetails = new HashSet<OvumTransferPair>();
        }

        public int SqlId { get; set; }
        public Guid OvumDetailId { get; set; }
        public Guid? OvumPickupId { get; set; }
        public int OvumNumber { get; set; }
        public int OvumDetailStatusId { get; set; }
        public Guid? OvumFreezeId { get; set; }
        public Guid? OvumThawId { get; set; }
        public Guid? FertilizationId { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public Guid? TransferInId { get; set; }
        public Guid OvumFromCourseOfTreatmentId { get; set; }

        public virtual CourseOfTreatment CourseOfTreatment { get; set; } = null!;
        public virtual Fertilization? Fertilization { get; set; }
        public virtual OvumDetailStatus OvumDetailStatus { get; set; } = null!;
        public virtual OvumFreeze? OvumFreeze { get; set; }
        public virtual CourseOfTreatment OvumFromCourseOfTreatment { get; set; } = null!;
        public virtual OvumPickup? OvumPickup { get; set; }
        public virtual OvumThaw? OvumThaw { get; set; }
        public virtual TransferIn? TransferIn { get; set; }
        public virtual ICollection<ObservationNote> ObservationNotes { get; set; }
        public virtual ICollection<OvumThawFreezePair> OvumThawFreezePairFreezeOvumDetails { get; set; }
        public virtual ICollection<OvumThawFreezePair> OvumThawFreezePairThawOvumDetails { get; set; }
        public virtual ICollection<OvumTransferPair> OvumTransferPairDonorOvumDetails { get; set; }
        public virtual ICollection<OvumTransferPair> OvumTransferPairRecipientOvumDetails { get; set; }
    }
}
