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
        public int StorageUnitId { get; set; }
        public bool IsThawed { get; set; }
        public Guid SpermFreezeSituationId { get; set; }

        public virtual CourseOfTreatment CourseOfTreatment { get; set; } = null!;
        public virtual SpermFreezeSituation SpermFreezeSituation { get; set; } = null!;
        public virtual StorageUnit StorageUnit { get; set; } = null!;
    }
}
