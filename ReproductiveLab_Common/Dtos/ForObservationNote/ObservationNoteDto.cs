using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForObservationNote
{
    public class ObservationNoteDto
    {
        public Guid ovumDetailId { get; set; }
        public DateTime? ovumPickupDate { get; set; }
        public int ovumNumber { get; set; }
        public List<Observation> observationNote { get; set; }
        public List<List<Observation>> orderedObservationNote { get; set; } = new List<List<Observation>>();
    }
    public class Observation
    {
        public Guid observationNoteId { get; set; }
        public string? observationType { get; set; }
        public int? day { get; set; }
        public DateTime? observationTime { get; set; }
        public string? mainPhoto { get; set; }
        public string? mainPhotoBase64 { get; set; }

    }
}
