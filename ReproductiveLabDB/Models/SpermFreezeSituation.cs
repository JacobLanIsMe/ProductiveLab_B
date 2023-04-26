using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class SpermFreezeSituation
    {
        public SpermFreezeSituation()
        {
            SpermFreezes = new HashSet<SpermFreeze>();
        }

        public int SqlId { get; set; }
        public Guid SpermFreezeSituationId { get; set; }
        public Guid Embryologist { get; set; }
        public DateTime FreezeTime { get; set; }
        public int SpermFreezeOperationMethodId { get; set; }
        public Guid FreezeMediumInUseId { get; set; }
        public string? OtherFreezeMediumName { get; set; }
        public Guid MediumInUseId1 { get; set; }
        public Guid? MediumInUseId2 { get; set; }
        public Guid? MediumInUseId3 { get; set; }

        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual MediumInUse FreezeMediumInUse { get; set; } = null!;
        public virtual MediumInUse MediumInUseId1Navigation { get; set; } = null!;
        public virtual MediumInUse? MediumInUseId2Navigation { get; set; }
        public virtual MediumInUse? MediumInUseId3Navigation { get; set; }
        public virtual SpermFreezeOperationMethod SpermFreezeOperationMethod { get; set; } = null!;
        public virtual ICollection<SpermFreeze> SpermFreezes { get; set; }
    }
}
