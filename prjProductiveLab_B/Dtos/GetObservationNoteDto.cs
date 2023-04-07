namespace prjProductiveLab_B.Dtos
{
    public class GetObservationNoteDto : AddObservationNoteDto
    {
        public List<observationNotePhotoDto>? observationNotePhotos { get; set; }
    }

    public class observationNotePhotoDto
    {
        public Guid observationNotePhotoId { get; set; }
        public string? route { get; set; }
        public bool isMainPhoto { get; set; }
        public string? imageBase64String { get; set; }
    }
}
