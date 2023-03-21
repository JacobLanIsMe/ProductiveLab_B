namespace prjProductiveLab_B.Dtos
{
    public class OriginInfoOfSpermDto : BaseCustomerInfoDto
    {
        public bool isFresh { get; set; }
        public string? spermRetrievalMethod { get; set; }

    }
}
