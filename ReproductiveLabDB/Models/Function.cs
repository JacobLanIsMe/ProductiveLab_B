using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class Function
    {
        public int SqlId { get; set; }
        public string Name { get; set; } = null!;
        public string Route { get; set; } = null!;
        public int FunctionTypeId { get; set; }
        public string? Icon { get; set; }
        public int ParentFunctionId { get; set; }

        public virtual FunctionType FunctionType { get; set; } = null!;
    }
}
