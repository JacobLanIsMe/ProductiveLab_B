using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Models;

namespace prjProductiveLab_B.Services
{
    public class ObservationNoteService: IObservationNoteService
    {
        private readonly ReproductiveLabContext dbContext;
        public ObservationNoteService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ObservationNoteDto>> GetObservationNote(Guid courseOfTreatmentId)
        {
            var result = await dbContext.OvumPickupDetails.Where(x=>x.OvumPickup.CourseOfTreatmentId == courseOfTreatmentId).Include(x => x.ObservationNotes).ThenInclude(x => x.ObservationNotePhotos).Select(x => new ObservationNoteDto
            {
                ovumPickupDate = x.OvumPickup.UpdateTime,
                ovumNumber = x.OvumNumber,
                observationNote = x.ObservationNotes.Where(y => y.OvumPickupDetail.OvumNumber == x.OvumNumber).Select(y => new Observation
                {
                    observationType = y.ObservationType.Name,
                    day = Convert.ToInt32(y.ObservationTime.Date - x.OvumPickup.UpdateTime),
                    observationTime = y.ObservationTime,
                    mainPhoto = y.ObservationNotePhotos.Where(z => z.IsMainPhoto == true).Select(z => z.Route).FirstOrDefault()
                }).OrderBy(y=>y.observationTime).ToList()
            }).OrderBy(x=>x.ovumNumber).ToListAsync();
            foreach (var i in result)
            {
                for (int j = 0; j < 7; j++)
                {
                    var note = i.observationNote.FirstOrDefault(x => x.day == j);
                    if (note == null)
                    {
                        i.orderedObservationNote.Add(new Observation());
                    }
                    else
                    {
                        i.orderedObservationNote.Add(note);
                    }
                }
            }
            return result;
        }
    }
}
