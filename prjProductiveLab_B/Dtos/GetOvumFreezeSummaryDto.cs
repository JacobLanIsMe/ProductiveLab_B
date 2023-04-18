using ReproductiveLabDB.Models;

namespace prjProductiveLab_B.Dtos
{
    public class GetOvumFreezeSummaryDto
    {
        public Guid courseOfTreatmentId { get; set; }
        public Guid? ovumFromCourseOfTreatmentId { get; set; }
        public int ovumNumber { get; set; }
        public DateTime ovumPickupTime { get; set; }
        public DateTime freezeTime { get; set; }
        public GetObservationNoteNameDto? freezeObservationNoteInfo { get; set; }
        public BaseStorage? freezeStorageInfo { get; set; }
        public string? medium { get; set; }
    }

}
