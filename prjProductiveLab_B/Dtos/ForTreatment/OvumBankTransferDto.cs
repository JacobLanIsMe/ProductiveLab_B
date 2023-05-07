namespace prjProductiveLab_B.Dtos.ForTreatment
{
    public class OvumBankTransferDto
    {
        public int recipientCourseOfTreatmentSqlId { get; set; }
        public Guid donorCourseOfTreatmentId { get; set; }
        public List<Guid>? transferOvumDetailIds { get; set; }
    }
}
