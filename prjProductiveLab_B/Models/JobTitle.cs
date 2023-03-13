using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class JobTitle
    {
        public JobTitle()
        {
            Employees = new HashSet<Employee>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
