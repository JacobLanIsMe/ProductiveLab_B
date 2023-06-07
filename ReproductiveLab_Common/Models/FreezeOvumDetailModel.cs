using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Models
{
    public class FreezeOvumDetailModel
    {
        public OvumFreeze? ovumFreeze { get; set; }
        public StorageUnit? storageUnit { get; set; }
        public OvumDetail? ovumDetail { get; set; }
        public int observationNoteCount { get; set; }
        public bool isTransferred { get; set; }
    }
}
