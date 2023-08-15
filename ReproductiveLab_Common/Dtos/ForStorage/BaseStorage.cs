using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForStorage
{
    public class BaseStorage
    {
        public StorageTankDto? tankInfo { get; set; }
        public int tankId { get; set; }
        public int canistId { get; set; }
        public string? canistName { get; set; }
        public int stripBoxId { get; set; }
        public string? stripBoxName { get; set; }
        public string? topColorName { get; set; }
        public StorageUnitDto? unitInfo { get; set; }
    }
    public class BaseStorageWithOvumDetailId : BaseStorage
    {
        public Guid OvumDetailId { get; set; }  
    }

}
