namespace prjProductiveLab_B.Dtos
{
    public class StorageUnitStatusDto
    {
        public int stripIdOrBoxId { get; set; }
        public string? stripNameOrBoxName { get; set; }
        public int stripBoxEmptyUnit { get; set; }
        public List<StorageUnitDto>? storageUnitInfo { get; set; }
    }
}
