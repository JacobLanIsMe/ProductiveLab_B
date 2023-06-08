using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForTransferIn
{
    public class EmbryoTansferInDto
    {
        public string? fertilizationResultId { get; set; }
        public string? blastomereScore_C_Id { get; set; }
        public string? blastomereScore_G_Id { get; set; }
        public string? blastomereScore_F_Id { get; set; }
        public List<int>? embryoStatusIds { get; set; }
        public string? blastocystScore_Expansion_Id { get; set; }
        public string? blastocystScore_ICE_Id { get; set; }
        public string? blastocystScore_TE_Id { get; set; }
        public string? day { get; set; }
        public string? kidScore { get; set; }
        public string? pgtaNumber { get; set; }
        public string? pgtaResult { get; set; }
        public string? pgtmResult { get; set; }
    }
}
