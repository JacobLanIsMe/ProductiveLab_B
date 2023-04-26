﻿using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class Incubator
    {
        public Incubator()
        {
            Fertilisations = new HashSet<Fertilisation>();
        }

        public int SqlId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Fertilisation> Fertilisations { get; set; }
    }
}
