using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForObservationNote
{
    public class ObservationNotePhotoDto
    {
        public Guid observationNotePhotoId { get; set; }
        public string? photoName { get; set; }
        public bool isMainPhoto { get; set; }
        public string? imageBase64String { get; set; }
    }
}
