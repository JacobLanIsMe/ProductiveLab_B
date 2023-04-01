namespace prjProductiveLab_B.Dtos
{
    public class BaseOperateSpermInfoDto
    {
        public BaseCustomerInfoDto? husband { get; set; }
        public bool isFresh { get; set; }
        public string? spermRetrievalMethod { get; set; }
        public BaseCustomerInfoDto? spermOwner { get; set; }
        public Guid? spermFromCourseOfTreatmentId { get; set; }
    }

    
    public class SpermFreezeDto
    {
        public Guid? spermFreezeId { get; set; }
        public int vialNumber { get; set; }
        public string? storageUnitName { get; set; }
        public string? storageCaneBoxName { get; set; }
        public string? storageShelfName { get; set; }
        public string? storageTankName { get; set; }
        public int storageUnitId { get; set; }
    }

    public class BaseOperateSpermInfo : BaseOperateSpermInfoDto
    {
        public int treatmentId { get; set; }
        
        // 來源精子的治療方式編號
        public int treatmentIdOfSpermFromCourseOfTreatmentId { get; set; }

    }
}
