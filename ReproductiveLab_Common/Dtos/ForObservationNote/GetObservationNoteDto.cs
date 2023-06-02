using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForObservationNote
{
    public class GetObservationNoteDto : AddObservationNoteDto
    {
        public List<ObservationNotePhotoDto>? observationNotePhotos { get; set; }
    }
}
