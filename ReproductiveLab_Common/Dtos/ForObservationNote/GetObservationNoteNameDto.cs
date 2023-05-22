using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForObservationNote
{
    public class GetObservationNoteNameDto : SharedObservationNote
    {
        public Guid? observationNoteId { get; set; }
        public string? embryologist { get; set; }
        public string? ovumMaturationName { get; set; }
        public string? observationTypeName { get; set; }
        public List<string>? ovumAbnormalityName { get; set; }
        public string? fertilizationResultName { get; set; }
        public string? blastomereScore_C_Name { get; set; }
        public string? blastomereScore_G_Name { get; set; }
        public string? blastomereScore_F_Name { get; set; }
        public List<string>? embryoStatusName { get; set; }
        public string? blastocystScore_Expansion_Name { get; set; }
        public string? blastocystScore_ICE_Name { get; set; }
        public string? blastocystScore_TE_Name { get; set; }
        public List<string>? operationTypeName { get; set; }
        public string? spindleResult { get; set; }
        public List<ObservationNotePhotoDto>? observationNotePhotos { get; set; }
    }
}
