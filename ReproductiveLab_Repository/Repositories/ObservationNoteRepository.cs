using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Repositories
{
    public class ObservationNoteRepository : IObservationNoteRepository
    {
        private readonly ReproductiveLabContext _db;
        private readonly IWebHostEnvironment _env;
        public ObservationNoteRepository(ReproductiveLabContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IQueryable<ObservationNote> GetObservationNotesByOvumDetailIds(List<Guid> ovumDetailIds)
        {
            return _db.ObservationNotes.Where(x => ovumDetailIds.Contains(x.OvumDetailId));
        }
        public List<ObservationNoteDto> GetObservationNote(Guid courseOfTreatmentId)
        {
            var result = _db.OvumDetails.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new ObservationNoteDto
            {
                ovumDetailId = x.OvumDetailId,
                ovumPickupDate = x.OvumPickup.UpdateTime,
                ovumNumber = x.OvumNumber,
                observationNote = x.ObservationNotes.Where(y => y.IsDeleted == false).Select(y => new Observation
                {
                    observationNoteId = y.ObservationNoteId,
                    observationType = y.ObservationType.Name,
                    day = y.Day,
                    observationTime = y.ObservationTime,
                    mainPhoto = y.ObservationNotePhotos.Where(z => z.IsMainPhoto == true && z.IsDeleted == false).Select(z => z.PhotoName).FirstOrDefault()
                }).ToList()
            }).OrderBy(x => x.ovumNumber).AsNoTracking().ToList();
            
            return result;
        }
        public List<Common1Dto> GetOvumMaturation()
        {
            return _db.OvumMaturations.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public List<Common1Dto> GetObservationType()
        {
            return _db.ObservationTypes.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public List<Common1Dto> GetOvumAbnormality()
        {
            return _db.OvumAbnormalities.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public List<Common1Dto> GetFertilizationResult()
        {
            return _db.FertilizationResults.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public async Task<List<Common1Dto>> GetBlastomereScoreC()
        {
            return await _db.BlastomereScoreCs.Select(x => new Common1Dto
            {
                id = x.SlqId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
        public async Task<List<Common1Dto>> GetBlastomereScoreG()
        {
            return await _db.BlastomereScoreGs.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
        public async Task<List<Common1Dto>> GetBlastomereScoreF()
        {
            return await _db.BlastomereScoreFs.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
        public List<Common1Dto> GetEmbryoStatus()
        {
            return _db.EmbryoStatuses.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public async Task<List<Common1Dto>> GetBlastocystScoreExpansion()
        {
            return await _db.BlastocystScoreExpansions.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
        public async Task<List<Common1Dto>> GetBlastocystScoreIce()
        {
            return await _db.BlastocystScoreIces.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
        public async Task<List<Common1Dto>> GetBlastocystScoreTe()
        {
            return await _db.BlastocystScoreTes.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
        public List<Common1Dto> GetOperationType()
        {
            return _db.OperationTypes.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public void AddObservationNote(ObservationNote input)
        {
            _db.ObservationNotes.Add(input);
            _db.SaveChanges();
        }
        public Guid GetLatestObservationNoteId()
        {
            return _db.ObservationNotes.OrderByDescending(x => x.SqlId).Select(x => x.ObservationNoteId).FirstOrDefault();
        }
        public void AddObservationNoteEmbryoStatus(Guid observationNoteId, List<int> embryoStatusIds)
        {
            foreach (var i in embryoStatusIds)
            {
                ObservationNoteEmbryoStatus embryoStatus = new ObservationNoteEmbryoStatus
                {
                    ObservationNoteId = observationNoteId,
                    EmbryoStatusId = i,
                    IsDeleted = false
                };
                _db.ObservationNoteEmbryoStatuses.Add(embryoStatus);
            }
            _db.SaveChanges();
        }
        public void AddObservationNoteOvumAbnormality(Guid observationNoteId, List<int> ovumAbnormalityIds)
        {
            foreach (var i in ovumAbnormalityIds)
            {
                ObservationNoteOvumAbnormality ovumAbnormality = new ObservationNoteOvumAbnormality
                {
                    ObservationNoteId = observationNoteId,
                    OvumAbnormalityId = i,
                    IsDeleted = false
                };
                _db.ObservationNoteOvumAbnormalities.Add(ovumAbnormality);
            }
            _db.SaveChanges();
        }
        public void AddObservationNoteOperation(Guid observationNoteId, AddObservationNoteDto input, List<int> operationTypeIds)
        {
            foreach (var i in operationTypeIds)
            {
                ObservationNoteOperation operation = new ObservationNoteOperation
                {
                    ObservationNoteId = observationNoteId,
                    OperationTypeId = i,
                    IsDeleted = false
                };
                if (i == (int)OperationTypeEnum.Spindle && input.spindleResult != "null")
                {
                    operation.SpindleResult = input.spindleResult;
                }
                _db.ObservationNoteOperations.Add(operation);
            }
            _db.SaveChanges();
        }
        public void AddObservationNotePhoto(List<IFormFile>? photos, Guid observationNoteId, bool hasAlreadyMainPhotoIndex, int mainPhotoIndex)
        {
            for (int i = 0; i < photos.Count; i++)
            {
                string pName = Guid.NewGuid() + ".png";
                string path = Path.Combine(_env.ContentRootPath, "uploads", "images", pName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    photos[i].CopyTo(stream);
                }
                ObservationNotePhoto photo = new ObservationNotePhoto
                {
                    ObservationNoteId = observationNoteId,
                    PhotoName = pName,
                    IsDeleted = false
                };
                if (hasAlreadyMainPhotoIndex)
                {
                    photo.IsMainPhoto = false;
                }
                else
                {
                    photo.IsMainPhoto = mainPhotoIndex == i ? true : false;
                }
                _db.ObservationNotePhotos.Add(photo);
            }
            _db.SaveChanges();
        }
        public ObservationNote? GetObservationNoteById(Guid observationNoteId)
        {
            return _db.ObservationNotes.FirstOrDefault(x => x.ObservationNoteId == observationNoteId);
        }
        public void deleteObservationNoteEmbryoStatus(Guid observationNoteId)
        {
            var q = _db.ObservationNoteEmbryoStatuses.Where(x => x.ObservationNoteId == observationNoteId);
            foreach (var i in q)
            {
                i.IsDeleted = true;
            }
            _db.SaveChanges();
        }
        public void deleteObservationNoteOperation(Guid observationNoteId)
        {
            var q = _db.ObservationNoteOperations.Where(x => x.ObservationNoteId == observationNoteId);
            foreach (var i in q)
            {
                i.IsDeleted = true;
            }
            _db.SaveChanges();
        }
        public void deleteObservationNoteOvumAbnormality(Guid observationNoteId)
        {
            var q = _db.ObservationNoteOvumAbnormalities.Where(x => x.ObservationNoteId == observationNoteId);
            foreach (var i in q)
            {
                i.IsDeleted = true;
            }
            _db.SaveChanges();
        }
        public IQueryable<ObservationNotePhoto> GetObservatioNotePhotosByObservationNoteId(Guid observationNoteId)
        {
            return _db.ObservationNotePhotos.Where(x => x.ObservationNoteId == observationNoteId && !x.IsDeleted);
        }
        public void UpdateObservationNotePhoto(IQueryable<ObservationNotePhoto> existingPhotos, List<ObservationNotePhotoDto> inputExistingPhotos)
        {
            foreach (var i in existingPhotos)
            {
                var q = inputExistingPhotos.FirstOrDefault(x => x.photoName == i.PhotoName);
                if (q != null)
                {
                    i.IsMainPhoto = q.isMainPhoto;
                }
                else
                {
                    i.IsMainPhoto = false;
                    i.IsDeleted = true;
                }
            }
            _db.SaveChanges();
        }
        public void DeleteObservationNotePhoto(IQueryable<ObservationNotePhoto> existingPhotos)
        {
            foreach (var i in existingPhotos)
            {
                i.IsMainPhoto = false;
                i.IsDeleted = true;
            }
            _db.SaveChanges();
        }
    }
}
