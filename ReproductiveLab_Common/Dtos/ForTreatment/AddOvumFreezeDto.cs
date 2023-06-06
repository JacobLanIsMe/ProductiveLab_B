﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForTreatment
{
    public class AddOvumFreezeDto
    {
        public List<Guid> ovumDetailId { get; set; }
        public DateTime freezeTime { get; set; }
        public Guid embryologist { get; set; }
        public int storageUnitId { get; set; }
        public Guid mediumInUseId { get; set; }
        public string? otherMediumName { get; set; }
        public int ovumMorphology_A { get; set; }
        public int ovumMorphology_B { get; set; }
        public int ovumMorphology_C { get; set; }
        public string? memo { get; set; }
        public int topColorId { get; set; }
    }
}
