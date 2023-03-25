namespace prjProductiveLab_B.Dtos
{
    public class StorageTankStatusDot
    {
        public StorageTankDto? tankInfo { get; set; }
        public int tankId { get; set; }
        public int shelfId { get; set; }
        public string? shelfName { get; set; }
        public int emptyAmount { get; set; }
        public int occupiedAmount { get; set; }
        public int totalAmount { get; set; }
    }
}
