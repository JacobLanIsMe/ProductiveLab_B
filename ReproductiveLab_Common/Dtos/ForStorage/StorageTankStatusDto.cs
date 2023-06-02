using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForStorage
{
    public class StorageTankStatusDto : BaseStorage
    {
        public int? emptyAmount { get; set; }
        public int? occupiedAmount { get; set; }
        public int? totalAmount { get; set; }
        public List<StorageUnitStatusDto>? unitInfos { get; set; }
    }
}
