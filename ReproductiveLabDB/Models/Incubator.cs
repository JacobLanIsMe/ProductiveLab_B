﻿using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class Incubator
    {
        public Incubator()
        {
            Fertilizations = new HashSet<Fertilization>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Fertilization> Fertilizations { get; set; }
    }
}
