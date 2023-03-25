namespace prjProductiveLab_B.Dtos
{
    public class StorageUnitStatusDto
    {
        public int caneIdOrBoxId { get; set; }
        public string? caneNameOrBoxName { get; set; }
        public int caneBoxEmptyUnit { get; set; }
        public List<StorageUnitDto>? storageUnitInfo { get; set; }
    }
}
