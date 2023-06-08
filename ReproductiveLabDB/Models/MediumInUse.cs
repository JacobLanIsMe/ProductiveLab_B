using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class MediumInUse
    {
        public MediumInUse()
        {
            FertilizationMediumInUseId1Navigations = new HashSet<Fertilization>();
            FertilizationMediumInUseId2Navigations = new HashSet<Fertilization>();
            FertilizationMediumInUseId3Navigations = new HashSet<Fertilization>();
            OvumFreezes = new HashSet<OvumFreeze>();
            OvumPickupMediumInUseId1Navigations = new HashSet<OvumPickup>();
            OvumPickupMediumInUseId2Navigations = new HashSet<OvumPickup>();
            OvumPickupMediumInUseId3Navigations = new HashSet<OvumPickup>();
            OvumThawMediumInUseId1Navigations = new HashSet<OvumThaw>();
            OvumThawMediumInUseId2Navigations = new HashSet<OvumThaw>();
            OvumThawMediumInUseId3Navigations = new HashSet<OvumThaw>();
            OvumThawThawMediumInUses = new HashSet<OvumThaw>();
            SpermFreezeSituationFreezeMediumInUses = new HashSet<SpermFreezeSituation>();
            SpermFreezeSituationMediumInUseId1Navigations = new HashSet<SpermFreezeSituation>();
            SpermFreezeSituationMediumInUseId2Navigations = new HashSet<SpermFreezeSituation>();
            SpermFreezeSituationMediumInUseId3Navigations = new HashSet<SpermFreezeSituation>();
            SpermThawMediumInUseId1Navigations = new HashSet<SpermThaw>();
            SpermThawMediumInUseId2Navigations = new HashSet<SpermThaw>();
            SpermThawMediumInUseId3Navigations = new HashSet<SpermThaw>();
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
        public virtual ICollection<Fertilization> FertilizationMediumInUseId1Navigations { get; set; }
        public virtual ICollection<Fertilization> FertilizationMediumInUseId2Navigations { get; set; }
        public virtual ICollection<Fertilization> FertilizationMediumInUseId3Navigations { get; set; }
        public virtual ICollection<OvumFreeze> OvumFreezes { get; set; }
        public virtual ICollection<OvumPickup> OvumPickupMediumInUseId1Navigations { get; set; }
        public virtual ICollection<OvumPickup> OvumPickupMediumInUseId2Navigations { get; set; }
        public virtual ICollection<OvumPickup> OvumPickupMediumInUseId3Navigations { get; set; }
        public virtual ICollection<OvumThaw> OvumThawMediumInUseId1Navigations { get; set; }
        public virtual ICollection<OvumThaw> OvumThawMediumInUseId2Navigations { get; set; }
        public virtual ICollection<OvumThaw> OvumThawMediumInUseId3Navigations { get; set; }
        public virtual ICollection<OvumThaw> OvumThawThawMediumInUses { get; set; }
        public virtual ICollection<SpermFreezeSituation> SpermFreezeSituationFreezeMediumInUses { get; set; }
        public virtual ICollection<SpermFreezeSituation> SpermFreezeSituationMediumInUseId1Navigations { get; set; }
        public virtual ICollection<SpermFreezeSituation> SpermFreezeSituationMediumInUseId2Navigations { get; set; }
        public virtual ICollection<SpermFreezeSituation> SpermFreezeSituationMediumInUseId3Navigations { get; set; }
        public virtual ICollection<SpermThaw> SpermThawMediumInUseId1Navigations { get; set; }
        public virtual ICollection<SpermThaw> SpermThawMediumInUseId2Navigations { get; set; }
        public virtual ICollection<SpermThaw> SpermThawMediumInUseId3Navigations { get; set; }
    }
}
