using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class Fertilization
    {
        public Fertilization()
        {
            OvumDetails = new HashSet<OvumDetail>();
        }

        public int SqlId { get; set; }
        public Guid FertilizationId { get; set; }
        public DateTime FertilizationTime { get; set; }
        public Guid Embryologist { get; set; }
        public int FertilizationMethodId { get; set; }
        public int IncubatorId { get; set; }
        public Guid MediumInUseId1 { get; set; }
        public Guid? MediumInUseId2 { get; set; }
        public Guid? MediumInUseId3 { get; set; }
        public string? OtherIncubator { get; set; }

        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual FertilizationMethod FertilizationMethod { get; set; } = null!;
        public virtual Incubator Incubator { get; set; } = null!;
        public virtual MediumInUse MediumInUseId1Navigation { get; set; } = null!;
        public virtual MediumInUse? MediumInUseId2Navigation { get; set; }
        public virtual MediumInUse? MediumInUseId3Navigation { get; set; }
        public virtual ICollection<OvumDetail> OvumDetails { get; set; }
    }
}
