using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForTreatment
{
    public class AddCourseOfTreatmentDto
    {
        public Guid doctorId { get; set; }
        public Guid customerId { get; set; }
        public string? ovumSituationId { get; set; }
        public string? ovumSourceId { get; set; }
        public string? ovumOperationId { get; set; }
        public string? spermSituationId { get; set; }
        public string? spermSourceId { get; set; }
        public string? spermOperationId { get; set; }
        public string? SpermRetrievalMethodId { get; set; }
        public string? embryoSituationId { get; set; }
        public string? embryoOperationId { get; set; }
        public DateTime surgicalTime { get; set; }
        public string? memo { get; set; }
    }
}
