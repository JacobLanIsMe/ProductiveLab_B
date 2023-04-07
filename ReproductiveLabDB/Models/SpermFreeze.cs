using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class SpermFreeze
    {
        public int SqlId { get; set; }
        public Guid SpermFreezeId { get; set; }
        public int VialNumber { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public Guid MediumInUseId1 { get; set; }
        public int StorageUnitId { get; set; }
        public Guid Embryologist { get; set; }
        public DateTime FreezeTime { get; set; }
        public int SpermFreezeOperationMethodId { get; set; }
        public Guid FreezeMediumInUseId { get; set; }
        public Guid? MediumInUseId2 { get; set; }
        public Guid? MediumInUseId3 { get; set; }
        public bool IsThawed { get; set; }

        public virtual CourseOfTreatment CourseOfTreatment { get; set; } = null!;
        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual MediumInUse FreezeMediumInUse { get; set; } = null!;
        public virtual MediumInUse MediumInUseId1Navigation { get; set; } = null!;
        public virtual MediumInUse? MediumInUseId2Navigation { get; set; }
        public virtual MediumInUse? MediumInUseId3Navigation { get; set; }
        public virtual SpermFreezeOperationMethod SpermFreezeOperationMethod { get; set; } = null!;
        public virtual StorageUnit StorageUnit { get; set; } = null!;
    }
}
