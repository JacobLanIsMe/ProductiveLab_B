using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;

namespace prjProductiveLab_B.Services
{
    public class SharedFunctionService: ISharedFunctionService
    {
        public void SetMediumInUse<T>(T mediumTable, List<Guid> inputMediums)
        {
            if (typeof(T) == typeof(SpermFreezeSituation))
            {
                if (inputMediums.Count > 0)
                {
                    (mediumTable as SpermFreezeSituation).MediumInUseId1 = inputMediums[0];
                }
                if (inputMediums.Count > 1)
                {
                    (mediumTable as SpermFreezeSituation).MediumInUseId2 = inputMediums[1];
                }
                if (inputMediums.Count > 2)
                {
                    (mediumTable as SpermFreezeSituation).MediumInUseId3 = inputMediums[2];
                }
            }
            if (typeof(T) == typeof(OvumPickup))
            {
                if (inputMediums.Count > 0)
                {
                    (mediumTable as OvumPickup).MediumInUseId1 = inputMediums[0];
                }
                if (inputMediums.Count > 1)
                {
                    (mediumTable as OvumPickup).MediumInUseId2 = inputMediums[1];
                }
                if (inputMediums.Count > 2)
                {
                    (mediumTable as OvumPickup).MediumInUseId3 = inputMediums[2];
                }
            }
        }
    }
}
