using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos
{
    public class AddCustomerDto
    {
        public string? name { get; set; }
        public int genderId { get; set; }
        public DateTime birthday { get; set; }
        public string? spouseName { get; set; }
        public int? spouseGenderId { get; set; }
        public DateTime? spouseBirthday { get; set; }
    }
}
