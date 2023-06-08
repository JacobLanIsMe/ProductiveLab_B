using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class IdentityServer
    {
        public int SqlId { get; set; }
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string Scope { get; set; } = null!;
    }
}
