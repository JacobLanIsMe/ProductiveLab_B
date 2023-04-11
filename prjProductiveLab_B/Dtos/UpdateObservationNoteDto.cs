namespace prjProductiveLab_B.Dtos
{
    public class UpdateObservationNoteDto : AddObservationNoteDto
    {
        public Guid observationNoteId { get; set; }
        public string? existingPhotos { get; set; }
    }
}
