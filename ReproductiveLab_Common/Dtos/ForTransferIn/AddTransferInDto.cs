using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForTransferIn
{
    public class AddTransferInDto
    {
        public Guid courseOfTreatmentId { get; set; }
        public DateTime transferInTime { get; set; }
        public Guid embryologist { get; set; }
        public string? transferInCellType { get; set; }
        public string? germSourceId { get; set; }
        public DateTime freezeTime { get; set; }
        public Guid freezeMediumId { get; set; }
        public string? otherFreezeMedium { get; set; }
        public int count { get; set; }
        public List<int>? storageUnitIds { get; set; }
        public List<OvumTransferInDto>? ovumInfos { get; set; }
        public List<EmbryoTansferInDto>? embryoInfos { get; set; }
    }
}
