using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Repositories
{
    public class ObservationNoteRepository
    {
        private readonly ReproductiveLabContext _dbContext;
        public ObservationNoteRepository(ReproductiveLabContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<ObservationNote> GetObservationNotesByOvumDetailIds(List<Guid> ovumDetailIds)
        {
            return _dbContext.ObservationNotes.Where(x => ovumDetailIds.Contains(x.OvumDetailId));
        }
    }
}
