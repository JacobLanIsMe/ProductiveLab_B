using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Models
{
    public class CustomerModel
    {
        public string? name { get; set; }
        public int genderId { get; set; }
        public DateTime birthday { get; set; }
        public Guid? spouse { get; set; }
        public CustomerModel(string name, int genderId, DateTime birthday, Guid? spouse = null) 
        {
            this.name = name;
            this.genderId = genderId;
            this.birthday = birthday;
            this.spouse = spouse;
        }

    }
}
