using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class OvumTransferPair
    {
        public int SqlId { get; set; }
        public Guid OvumTransferPairId { get; set; }
        public Guid RecipientOvumDetailId { get; set; }
        public Guid DonorOvumDetailId { get; set; }

        public virtual OvumDetail DonorOvumDetail { get; set; } = null!;
        public virtual OvumDetail RecipientOvumDetail { get; set; } = null!;
    }
}
