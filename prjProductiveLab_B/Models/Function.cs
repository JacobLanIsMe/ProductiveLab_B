using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class Function
    {
        public Function()
        {
            SubFunctions = new HashSet<SubFunction>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }

        public virtual ICollection<SubFunction> SubFunctions { get; set; }
    }
}
