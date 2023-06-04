using ReproductiveLab_Common.Dtos.ForObservationNote;
using ReproductiveLab_Common.Dtos.ForStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForTreatment
{
    public class TreatmentSummaryDto
    {
        public Guid? ovumDetailId { get; set; }
        public int courseOfTreatmentSqlId { get; set; }
        public int? ovumFromCourseOfTreatmentSqlId { get; set; }
        public string? ovumDetailStatus { get; set; }
        public int dateOfEmbryo { get; set; }
        public int ovumNumber { get; set; }
        public DateTime? fertilizationTime { get; set; }
        public string? fertilizationMethod { get; set; }
        public GetObservationNoteNameDto? observationNote { get; set; }
        public string? ovumSource { get; set; }
        public BaseStorage? freezeStorageInfo { get; set; }
    }
}
