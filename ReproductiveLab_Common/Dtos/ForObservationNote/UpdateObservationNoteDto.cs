using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForObservationNote
{
    public class UpdateObservationNoteDto : AddObservationNoteDto
    {
        public Guid observationNoteId { get; set; }
        public string? existingPhotos { get; set; }
    }
}
