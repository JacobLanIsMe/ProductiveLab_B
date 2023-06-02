using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForOperateSperm
{
    public class AddSpermFreezeDto
    {
        public Guid courseOfTreatmentId { get; set; }
        public Guid embryologist { get; set; }
        public Guid freezeMedium { get; set; }
        public DateTime freezeTime { get; set; }
        public List<Guid>? mediumInUseArray { get; set; }
        public int spermFreezeOperationMethodId { get; set; }
        public List<int>? storageUnitId { get; set; }
        public string? otherFreezeMediumName { get; set; }
    }
}
