namespace prjProductiveLab_B.Dtos
{
    public class StorageAddNewTankDto : StorageTankDto
    {
        
        public int shelfAmount { get; set; }
        public int caneBoxAmount { get; set; }
        public int unitAmount { get; set; }
    }
}
