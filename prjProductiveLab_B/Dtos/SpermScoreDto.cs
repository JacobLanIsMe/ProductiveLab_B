namespace prjProductiveLab_B.Dtos
{
    public class SpermScoreDto
    {
        public decimal volume { get; set; }
        public decimal concentration { get; set; }
        public decimal activityA { get; set; }
        public decimal activityB { get; set; }
        public decimal activityC { get; set; }
        public decimal activityD { get; set; }
        public decimal? morphology { get; set; }
        public int? abstinence { get; set; }
        public int spermScoreTimePointId { get; set; }
        public DateTime recordTime { get; set; }
        public Guid embryologist { get; set; }
        public Guid courseOfTreatmentId { get; set; }
    }
}
