namespace prjProductiveLab_B.Dtos
{
    public class StorageTankStatusDto : BaseStorage
    {
        public int? emptyAmount { get; set; }
        public int? occupiedAmount { get; set; }
        public int? totalAmount { get; set; }
        public List<StorageUnitStatusDto>? unitInfos { get; set; }
    }

    public class BaseStorage
    {
        public StorageTankDto? tankInfo { get; set; }
        public int tankId { get; set; }
        public int canistId { get; set; }
        public string? canistName { get; set; }
        public int stripBoxId { get; set; }
        public string? stripBoxName { get; set; }
    }

    public class OvumFreezeStorageDto : BaseStorage
    {
        public int stripBoxEmptyUnit { get; set; }
        public List<StorageUnitDto>? storageUnitInfo { get; set; }
    }
}
