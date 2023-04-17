namespace prjProductiveLab_B.Dtos
{
    public class GetOvumFreezeSummaryDto
    {
        public Guid courseOfTreatmentId { get; set; }
        public Guid? ovumFromCourseOfTreatmentId { get; set; }
        public int ovumNumber { get; set; }
        public DateTime ovumPickupTime { get; set; }
        public DateTime freezeTime { get; set; }
        public FreezeObservationNote? freezeObservationNoteInfo { get; set; }
        public BaseStorage? freezeStorageInfo { get; set; }
        public string? medium { get; set; }
    }

    public class FreezeObservationNote
    {
        public int day { get; set; }
        public string? memo { get; set; }
        public string? freezeObservationNoteMainPhotoName { get; set; }
        public string? photoBase64String { get; set; }
    }

}
