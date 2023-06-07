using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Models
{
    public class ThawedOvumDetailModel
    {
        public int courseOfTreatmentSqlId { get; set; }
        public List<int>? ovumNumbers { get; set; }
    }
}
