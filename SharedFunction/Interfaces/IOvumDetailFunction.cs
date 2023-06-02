using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Interfaces
{
    public interface IOvumDetailFunction
    {
        List<GetOvumFreezeSummaryDto> GetOvumDetailInfos(IQueryable<OvumDetail> ovumDetails);
    }
}
