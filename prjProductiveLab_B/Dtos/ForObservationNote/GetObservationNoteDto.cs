namespace prjProductiveLab_B.Dtos.ForObservationNote
{
    public class GetObservationNoteDto : AddObservationNoteDto
    {
        public List<ObservationNotePhotoDto>? observationNotePhotos { get; set; }
    }

    public class ObservationNotePhotoDto
    {
        public Guid observationNotePhotoId { get; set; }
        public string? photoName { get; set; }
        public bool isMainPhoto { get; set; }
        public string? imageBase64String { get; set; }
    }
}
