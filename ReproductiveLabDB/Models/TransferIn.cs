using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class TransferIn
    {
        public TransferIn()
        {
            OvumDetails = new HashSet<OvumDetail>();
        }

        public int SqlId { get; set; }
        public Guid TransferInId { get; set; }
        public DateTime TransferInTime { get; set; }

        public virtual ICollection<OvumDetail> OvumDetails { get; set; }
    }
}
