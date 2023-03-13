using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class IdentityServer
    {
        public IdentityServer()
        {
            Employees = new HashSet<Employee>();
        }

        public int SqlId { get; set; }
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string Scope { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
