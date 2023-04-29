﻿using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OvumThaw
    {
        public OvumThaw()
        {
            OvumPickupDetails = new HashSet<OvumPickupDetail>();
            OvumThawFreezePairs = new HashSet<OvumThawFreezePair>();
        }

        public int SqlId { get; set; }
        public Guid OvumThawId { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public DateTime ThawTime { get; set; }
        public Guid ThawMediumInUseId { get; set; }
        public Guid MediumInUseId1 { get; set; }
        public Guid? MediumInUseId2 { get; set; }
        public Guid? MediumInUseId3 { get; set; }
        public Guid Embryologist { get; set; }
        public Guid RecheckEmbryologist { get; set; }

        public virtual CourseOfTreatment CourseOfTreatment { get; set; } = null!;
        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual MediumInUse MediumInUseId1Navigation { get; set; } = null!;
        public virtual MediumInUse? MediumInUseId2Navigation { get; set; }
        public virtual MediumInUse? MediumInUseId3Navigation { get; set; }
        public virtual Employee RecheckEmbryologistNavigation { get; set; } = null!;
        public virtual MediumInUse ThawMediumInUse { get; set; } = null!;
        public virtual ICollection<OvumPickupDetail> OvumPickupDetails { get; set; }
        public virtual ICollection<OvumThawFreezePair> OvumThawFreezePairs { get; set; }
    }
}
