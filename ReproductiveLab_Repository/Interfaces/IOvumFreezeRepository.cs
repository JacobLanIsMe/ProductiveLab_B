using ReproductiveLab_Common.Dtos.ForTreatment;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface IOvumFreezeRepository
    {
        void AddOvumFreeze(AddOvumFreezeDto input);
        Guid GetLatestOvumFreezedId();
        OvumFreeze? GetOvumFreezeByOvumDetailId(Guid ovumDetailId);
        void UpdateOvumFreeze(OvumFreeze ovumFreeze, AddOvumFreezeDto input);
        AddOvumFreezeDto? GetOvumFreezeDtoByOvumDetailId(Guid ovumDetailId);
    }
}
