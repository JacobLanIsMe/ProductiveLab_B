namespace prjProductiveLab_B.Dtos
{
    public class GetObservationNoteDto : AddObservationNoteDto
    {
        public List<ObservationNotePhotoDto>? observationNotePhotos { get; set; }
    }

    public class ObservationNotePhotoDto
    {
        public Guid observationNotePhotoId { get; set; }
        public string? route { get; set; }
        public bool isMainPhoto { get; set; }
        public string? imageBase64String { get; set; }
    }
}
