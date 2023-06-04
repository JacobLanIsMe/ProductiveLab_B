using ReproductiveLab_Common.Dtos.ForTreatment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Interfaces
{
    public interface ITreatmentFunction
    {
        string OvumPickupNoteValidation(AddOvumPickupNoteDto ovumPickupNote);
    }
}
