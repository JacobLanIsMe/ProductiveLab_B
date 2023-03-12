using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class JobTitle
    {
        public JobTitle()
        {
            staff = new HashSet<staff>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<staff> staff { get; set; }
    }
}
