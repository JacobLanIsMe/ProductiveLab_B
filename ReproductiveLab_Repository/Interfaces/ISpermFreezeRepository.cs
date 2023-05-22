using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface ISpermFreezeRepository
    {
        List<GetSpermFreezeSummaryDto> GetSpermFreezeSummary(Guid customerId);
    }
}
