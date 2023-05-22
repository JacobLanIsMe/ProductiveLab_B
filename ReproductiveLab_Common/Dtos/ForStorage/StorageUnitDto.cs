using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForStorage
{
    public class StorageUnitDto
    {
        public int storageUnitId { get; set; }
        public string? unitName { get; set; }
        public bool isOccupied { get; set; }
    }
}
