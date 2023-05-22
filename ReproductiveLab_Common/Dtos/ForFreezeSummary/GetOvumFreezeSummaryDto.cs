using ReproductiveLab_Common.Dtos.ForObservationNote;
using ReproductiveLab_Common.Dtos.ForStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForFreezeSummary
{
    public class GetOvumFreezeSummaryDto
    {
        public int courseOfTreatmentSqlId { get; set; }
        public Guid courseOfTreatmentId { get; set; }
        public int? ovumFromCourseOfTreatmentSqlId { get; set; }
        public Guid? ovumFromCourseOfTreatmentId { get; set; }
        public string? ovumSource { get; set; }
        public BaseCustomerInfoDto? ovumSourceOwner { get; set; }
        public Guid ovumDetailId { get; set; }
        public int ovumNumber { get; set; }
        public DateTime? ovumPickupTime { get; set; }
        public DateTime? freezeTime { get; set; }
        public DateTime? thawTime { get; set; }
        public GetObservationNoteNameDto? freezeObservationNoteInfo { get; set; }
        public BaseStorage? freezeStorageInfo { get; set; }
        public string? medium { get; set; }
        public bool isThawed { get; set; }
    }
}
