using System;
using System.Collections.Generic;

namespace prjProductiveLab_B.Models
{
    public partial class ObservationNotePhoto
    {
        public int SqlId { get; set; }
        public Guid ObservationNotePhotoId { get; set; }
        public Guid ObservationNoteId { get; set; }
        public bool IsMainPhoto { get; set; }
        public string Route { get; set; } = null!;

        public virtual ObservationNote ObservationNote { get; set; } = null!;
    }
}
