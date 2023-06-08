using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForTreatment
{
    public class OvumBankTransferDto
    {
        public int recipientCourseOfTreatmentSqlId { get; set; }
        public Guid donorCourseOfTreatmentId { get; set; }
        public List<Guid>? transferOvumDetailIds { get; set; }
    }
}
