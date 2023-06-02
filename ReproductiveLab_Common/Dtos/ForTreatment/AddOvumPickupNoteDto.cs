using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForTreatment
{
    public class AddOvumPickupNoteDto
    {
        public Guid courseOfTreatmentId { get; set; }
        public OperationTime? operationTime { get; set; }
        public OvumPickupNumber? ovumPickupNumber { get; set; }
        public string? embryologist { get; set; }
        public List<Guid>? mediumInUse { get; set; }
    }
    public class OperationTime
    {
        public DateTime? triggerTime { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
    }
    public class OvumPickupNumber
    {
        public int totalOvumNumber { get; set; }
        public int coc_Grade5 { get; set; } = 0;
        public int coc_Grade4 { get; set; } = 0;
        public int coc_Grade3 { get; set; } = 0;
        public int coc_Grade2 { get; set; } = 0;
        public int coc_Grade1 { get; set; } = 0;
    }
}
