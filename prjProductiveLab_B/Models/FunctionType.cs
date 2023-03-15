using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class FunctionType
    {
        public FunctionType()
        {
            Functions = new HashSet<Function>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Function> Functions { get; set; }
    }
}
