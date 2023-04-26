using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class SpermThaw
    {
        public SpermThaw()
        {
            SpermThawFreezePairs = new HashSet<SpermThawFreezePair>();
        }

        public int SqlId { get; set; }
        public Guid SpermThawId { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public int SpermThawMethodId { get; set; }
        public DateTime ThawTime { get; set; }
        public Guid Embryologist { get; set; }
        public Guid RecheckEmbryologist { get; set; }
        public string? OtherSpermThawMethod { get; set; }
        public Guid MediumInUseId1 { get; set; }
        public Guid? MediumInUseId2 { get; set; }
        public Guid? MediumInUseId3 { get; set; }

        public virtual CourseOfTreatment CourseOfTreatment { get; set; } = null!;
        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual MediumInUse MediumInUseId1Navigation { get; set; } = null!;
        public virtual MediumInUse? MediumInUseId2Navigation { get; set; }
        public virtual MediumInUse? MediumInUseId3Navigation { get; set; }
        public virtual Employee RecheckEmbryologistNavigation { get; set; } = null!;
        public virtual SpermThawMethod SpermThawMethod { get; set; } = null!;
        public virtual ICollection<SpermThawFreezePair> SpermThawFreezePairs { get; set; }
    }
}
