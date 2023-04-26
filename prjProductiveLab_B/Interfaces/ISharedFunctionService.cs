namespace prjProductiveLab_B.Interfaces
{
    public interface ISharedFunctionService
    {
        void SetMediumInUse<T>(T mediumTable, List<Guid> inputMediums);
    }
}
