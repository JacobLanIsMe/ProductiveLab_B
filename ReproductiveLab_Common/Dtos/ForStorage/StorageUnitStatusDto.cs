using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForStorage
{
    public class StorageUnitStatusDto
    {
        public int stripIdOrBoxId { get; set; }
        public string? stripNameOrBoxName { get; set; }
        public int stripBoxEmptyUnit { get; set; }
        public List<StorageUnitDto>? storageUnitInfo { get; set; }
    }
}
