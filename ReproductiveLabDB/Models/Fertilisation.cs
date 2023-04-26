using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class Fertilisation
    {
        public Fertilisation()
        {
            OvumPickupDetails = new HashSet<OvumPickupDetail>();
        }

        public int SqlId { get; set; }
        public Guid FertilisationId { get; set; }
        public DateTime FertilisationTime { get; set; }
        public Guid Embryologist { get; set; }
        public int FertilisationMethodId { get; set; }
        public int IncubatorId { get; set; }
        public Guid MediumInUseId1 { get; set; }
        public Guid? MediumInUseId2 { get; set; }
        public Guid? MediumInUseId3 { get; set; }
        public string? OtherIncubator { get; set; }

        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual FertilisationMethod FertilisationMethod { get; set; } = null!;
        public virtual Incubator Incubator { get; set; } = null!;
        public virtual MediumInUse MediumInUseId1Navigation { get; set; } = null!;
        public virtual MediumInUse? MediumInUseId2Navigation { get; set; }
        public virtual MediumInUse? MediumInUseId3Navigation { get; set; }
        public virtual ICollection<OvumPickupDetail> OvumPickupDetails { get; set; }
    }
}
