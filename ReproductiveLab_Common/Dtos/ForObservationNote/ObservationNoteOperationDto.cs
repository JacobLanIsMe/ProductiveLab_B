using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForObservationNote
{
    public class ObservationNoteOperationDto
    {
        public string operationTypeName { get; set; }
        public int operationTypeId { get; set; }
        public string? spindleResult { get; set; }
    }
}
