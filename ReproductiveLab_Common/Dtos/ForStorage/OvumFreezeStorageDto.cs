using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForStorage
{
    public class OvumFreezeStorageDto : BaseStorage
    {
        public int stripBoxEmptyUnit { get; set; }
        public List<StorageUnitDto>? storageUnitInfo { get; set; }
    }
}
