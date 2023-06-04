using ReproductiveLab_Common.Dtos.ForTreatment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Services
{
    public class TreatmentFunction
    {
        public string OvumPickupNoteValidation(AddOvumPickupNoteDto ovumPickupNote)
        {
            string errorMessage = "";
            if (ovumPickupNote.operationTime == null)
            {
                errorMessage += "操作時間有誤。\n";
            }
            if (ovumPickupNote.ovumPickupNumber == null)
            {
                errorMessage += "取卵結果有誤。\n";
            }
            else
            {
                int coc_grade5 = ovumPickupNote.ovumPickupNumber.coc_Grade5;
                int coc_grade4 = ovumPickupNote.ovumPickupNumber.coc_Grade4;
                int coc_grade3 = ovumPickupNote.ovumPickupNumber.coc_Grade3;
                int coc_grade2 = ovumPickupNote.ovumPickupNumber.coc_Grade2;
                int coc_grade1 = ovumPickupNote.ovumPickupNumber.coc_Grade1;
                if ((coc_grade1 + coc_grade2 + coc_grade3 + coc_grade4 + coc_grade5) != ovumPickupNote.ovumPickupNumber.totalOvumNumber)
                {
                    errorMessage += "取卵數總和有誤。\n";
                }
            }
            if (!Guid.TryParse(ovumPickupNote.embryologist, out Guid embryologist))
            {
                errorMessage += "胚胎師選項有誤。\n";
            }
            if (ovumPickupNote.mediumInUse == null || ovumPickupNote.mediumInUse.Count <= 0)
            {
                errorMessage += "培養液資訊有誤。\n";
            }
            return errorMessage;
        }
    }
}
