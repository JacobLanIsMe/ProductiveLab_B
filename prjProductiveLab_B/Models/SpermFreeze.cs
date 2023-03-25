using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class SpermFreeze
    {
        public int SqlId { get; set; }
        public Guid SpermFreezeId { get; set; }
        public int VialNumber { get; set; }
        public Guid SpermScoreId { get; set; }
        public Guid MediumInUseId { get; set; }
        public int StorageUnitId { get; set; }
        public Guid Embryologist { get; set; }
        public DateTime FreezeTime { get; set; }
        public int SpermFreezeOperationMethodId { get; set; }

        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual MediumInUse MediumInUse { get; set; } = null!;
        public virtual SpermFreezeOperationMethod SpermFreezeOperationMethod { get; set; } = null!;
        public virtual SpermScore SpermScore { get; set; } = null!;
        public virtual StorageUnit StorageUnit { get; set; } = null!;
    }
}
