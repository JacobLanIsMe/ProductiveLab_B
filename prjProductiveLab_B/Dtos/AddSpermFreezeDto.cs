namespace prjProductiveLab_B.Dtos
{
    public class AddSpermFreezeDto
    {
        public Guid courseOfTreatmentId { get; set; }
        public Guid embryologist { get; set; }
        public Guid freezeMedium { get; set; }
        public DateTime freezeTime { get; set; }
        public List<Guid>? mediumInUseArray { get; set; }
        public int spermFreezeOperationMethodId { get; set; }
        public List<int>? storageUnitId { get; set; }
        public string? otherFreezeMediumName { get; set; }
    }
}
