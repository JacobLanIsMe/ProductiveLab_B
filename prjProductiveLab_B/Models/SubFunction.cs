using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class SubFunction
    {
        public int SqlId { get; set; }
        public int FunctionId { get; set; }
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }

        public virtual Function Function { get; set; } = null!;
    }
}
