using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OvumFreeze
    {
        public OvumFreeze()
        {
            OvumDetails = new HashSet<OvumDetail>();
        }

        public int SqlId { get; set; }
        public Guid OvumFreezeId { get; set; }
        public DateTime FreezeTime { get; set; }
        public Guid Embryologist { get; set; }
        public int StorageUnitId { get; set; }
        public Guid MediumInUseId { get; set; }
        public string? Memo { get; set; }
        public string? OtherMediumName { get; set; }
        public int OvumMorphologyA { get; set; }
        public int OvumMorphologyB { get; set; }
        public int OvumMorphologyC { get; set; }
        public int TopColorId { get; set; }
        public bool IsThawed { get; set; }

        public virtual Employee EmbryologistNavigation { get; set; } = null!;
        public virtual MediumInUse MediumInUse { get; set; } = null!;
        public virtual StorageUnit StorageUnit { get; set; } = null!;
        public virtual TopColor TopColor { get; set; } = null!;
        public virtual ICollection<OvumDetail> OvumDetails { get; set; }
    }
}
