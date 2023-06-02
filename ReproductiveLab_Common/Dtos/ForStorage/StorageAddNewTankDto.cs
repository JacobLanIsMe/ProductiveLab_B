using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForStorage
{
    public class StorageAddNewTankDto : StorageTankDto
    {
        public int canistAmount { get; set; }
        public int stripBoxAmount { get; set; }
        public int unitAmount { get; set; }
    }
}
