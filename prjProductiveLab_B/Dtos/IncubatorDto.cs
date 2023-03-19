namespace prjProductiveLab_B.Dtos
{
    public class IncubatorDto : BaseResponseDto
    {
        public List<IncubatorViewModel>? data { get; set; }
    }
    public class IncubatorViewModel
    {
        public int incubatorId { get; set; }
        public string? name { get; set; }
    }
}
