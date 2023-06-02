using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForOperateSperm
{
    public class SpermFreezeDto
    {
        public Guid? spermFreezeId { get; set; }
        public int vialNumber { get; set; }
        public string? storageUnitName { get; set; }
        public int? storageStripBoxId { get; set; }
        public string? storageCanistName { get; set; }
        public string? storageTankName { get; set; }
        public int storageUnitId { get; set; }
        public DateTime freezeTime { get; set; }
    }
}
