using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class MediumInUse
    {
        public MediumInUse()
        {
            OvumFreezes = new HashSet<OvumFreeze>();
            OvumPickupDetails = new HashSet<OvumPickupDetail>();
            SpermFreezeSituationFreezeMediumInUses = new HashSet<SpermFreezeSituation>();
            SpermFreezeSituationMediumInUseId1Navigations = new HashSet<SpermFreezeSituation>();
            SpermFreezeSituationMediumInUseId2Navigations = new HashSet<SpermFreezeSituation>();
            SpermFreezeSituationMediumInUseId3Navigations = new HashSet<SpermFreezeSituation>();
        }

        public int SqlId { get; set; }
        public Guid MediumInUseId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime OpenDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string LotNumber { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int? MediumTypeId { get; set; }

        public virtual MediumType? MediumType { get; set; }
        public virtual ICollection<OvumFreeze> OvumFreezes { get; set; }
        public virtual ICollection<OvumPickupDetail> OvumPickupDetails { get; set; }
        public virtual ICollection<SpermFreezeSituation> SpermFreezeSituationFreezeMediumInUses { get; set; }
        public virtual ICollection<SpermFreezeSituation> SpermFreezeSituationMediumInUseId1Navigations { get; set; }
        public virtual ICollection<SpermFreezeSituation> SpermFreezeSituationMediumInUseId2Navigations { get; set; }
        public virtual ICollection<SpermFreezeSituation> SpermFreezeSituationMediumInUseId3Navigations { get; set; }
    }
}
