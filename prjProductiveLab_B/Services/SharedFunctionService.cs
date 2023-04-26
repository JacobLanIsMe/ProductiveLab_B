using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;

namespace prjProductiveLab_B.Services
{
    public class SharedFunctionService: ISharedFunctionService
    {
        public void SetMediumInUse<T>(T mediumTable, List<Guid> inputMediums)
        {
            
            if (inputMediums.Count > 0)
            {
                mediumTable.GetType().GetProperty("MediumInUseId1").SetValue(mediumTable, inputMediums[0]);
            }
            if (inputMediums.Count > 1)
            {
                mediumTable.GetType().GetProperty("MediumInUseId2").SetValue(mediumTable, inputMediums[1]);
            }
            if (inputMediums.Count > 2)
            {
                mediumTable.GetType().GetProperty("MediumInUseId3").SetValue(mediumTable, inputMediums[2]);
            }
        }
    }
}
