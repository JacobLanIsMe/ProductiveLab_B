using ReproductiveLab_Common.Dtos.ForTransferIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface ITransferInRepository
    {
        void AddTransferIn(AddTransferInDto input);
    }
}
