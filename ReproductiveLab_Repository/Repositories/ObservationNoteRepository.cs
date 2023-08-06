using Dapper;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ReproductiveLab_Repository.Repositories
{
    public class ObservationNoteRepository : IObservationNoteRepository
    {
        private readonly ReproductiveLabContext _db;
        //private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private string? imgurClientId = null;
        private string? imgurClientSecret = null;
        private string? dbConnectionString = null;
        
        public ObservationNoteRepository(ReproductiveLabContext db, IWebHostEnvironment env, IConfiguration config)
        {
            _db = db;
            _env = env;
            imgurClientId = config.GetSection("Imgur")["ClientId"];
            imgurClientSecret = config.GetSection("Imgur")["ClientSecret"];
            dbConnectionString = config.GetConnectionString("DefaultConnection");
        }
        public IQueryable<ObservationNote> GetObservationNotesByOvumDetailIds(List<Guid> ovumDetailIds)
        {
            return _db.ObservationNotes.Where(x => ovumDetailIds.Contains(x.OvumDetailId));
        }
        public List<ObservationNoteDto> GetObservationNoteByCourseOfTreatmentId(Guid courseOfTreatmentId)
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
        public List<Common1Dto> GetBlastomereScoreC()
        {
            var result = _db.BlastomereScoreCs.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
            return result;
        }
        public List<Common1Dto> GetBlastomereScoreG()
        {
            var result = _db.BlastomereScoreGs.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
            return result;
        }
        public List<Common1Dto> GetBlastomereScoreF()
        {
            var result = _db.BlastomereScoreFs.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
            return result;
        }
        public List<Common1Dto> GetEmbryoStatus()
        {
            return _db.EmbryoStatuses.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public List<Common1Dto> GetBlastocystScoreExpansion()
        {
            return _db.BlastocystScoreExpansions.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public List<Common1Dto> GetBlastocystScoreIce()
        {
            return _db.BlastocystScoreIces.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public List<Common1Dto> GetBlastocystScoreTe()
        {
            return _db.BlastocystScoreTes.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public List<Common1Dto> GetOperationType()
        {
            return _db.OperationTypes.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public void AddObservationNote(AddObservationNoteDto input)
        {
            ObservationNote observationNote = GenerateObservationNote(new ObservationNote(), input);
            _db.ObservationNotes.Add(observationNote);
            _db.SaveChanges();
        }
        public void UpdateObservationNote(ObservationNote existingObservationNote, AddObservationNoteDto input)
        {
            GenerateObservationNote(existingObservationNote, input);
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
        public async void AddObservationNotePhoto(List<IFormFile>? photos, Guid observationNoteId, bool hasAlreadyMainPhotoIndex, int mainPhotoIndex)
        {
            if (string.IsNullOrEmpty(imgurClientId) || string.IsNullOrEmpty(imgurClientSecret))
            {
                throw new Exception("ImgurClientId or ImgurClientSecret cannot be null");
            }
            var apiClient = new ApiClient(imgurClientId, imgurClientSecret);
            List<HttpClient> httpClients = new List<HttpClient>();
            List<MemoryStream> memoryStreams = new List<MemoryStream>();
            foreach (var photo in photos)
            {
                HttpClient httpClient = new HttpClient();
                httpClients.Add(httpClient);
                MemoryStream stream = new MemoryStream();
                await photo.CopyToAsync(stream);
                memoryStreams.Add(stream);
            }
            List<ObservationNotePhoto> observationNotePhotos = new List<ObservationNotePhoto>();
            for (int i = 0; i < memoryStreams.Count; i++)
            {
                memoryStreams[i].Seek(0, SeekOrigin.Begin);
                var imageEndpoint = new ImageEndpoint(apiClient, httpClients[i]);
                IImage imageUpload = await imageEndpoint.UploadImageAsync(memoryStreams[i]);
                string imageUrl = imageUpload.Link;
                ObservationNotePhoto photo = new ObservationNotePhoto
                {
                    ObservationNoteId = observationNoteId,
                    PhotoName = imageUrl,
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
                observationNotePhotos.Add(photo);
            }
            InsertObservationNotePhoto(observationNotePhotos);
        }
        private void InsertObservationNotePhoto(List<ObservationNotePhoto> observationNotePhotos)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                string sqlCommand = @"INSERT INTO [ObservationNotePhoto]
           ([ObservationNoteId]
           ,[IsMainPhoto]
           ,[PhotoName]
           ,[IsDeleted])
     VALUES
           (@ObservationNoteId
           ,@IsMainPhoto
           ,@PhotoName
           ,@IsDeleted)";
                using (var conn = new SqlConnection(dbConnectionString))
                {
                    conn.Execute(sqlCommand, observationNotePhotos);
                }
                scope.Complete();
            }

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
        public void DeleteObservationNotePhoto(Guid observationNoteId)
        {
            var observationNotePhotos = GetObservatioNotePhotosByObservationNoteId(observationNoteId);
            foreach (var i in observationNotePhotos)
            {
                i.IsDeleted = true;
            }
            _db.SaveChanges();
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
        public GetObservationNoteDto? GetExistingObservationNote(Guid observationNoteId)
        {
            return _db.ObservationNotes.Where(x => x.ObservationNoteId == observationNoteId).Select(x => new GetObservationNoteDto
            {
                ovumDetailId = x.OvumDetailId,
                observationTime = x.ObservationTime,
                embryologist = x.Embryologist,
                ovumMaturationId = x.OvumMaturationId.ToString(),
                observationTypeId = x.ObservationTypeId.ToString(),
                ovumAbnormalityIds = x.ObservationNoteOvumAbnormalities.Where(y => y.IsDeleted == false).Select(y => y.OvumAbnormalityId).ToList(),
                fertilizationResultId = x.FertilizationResultId.ToString(),
                blastomereScore_C_Id = x.BlastomereScoreCId.ToString(),
                blastomereScore_G_Id = x.BlastomereScoreGId.ToString(),
                blastomereScore_F_Id = x.BlastomereScoreFId.ToString(),
                embryoStatusIds = x.ObservationNoteEmbryoStatuses.Where(y => y.IsDeleted == false).Select(y => y.EmbryoStatusId).ToList(),
                blastocystScore_Expansion_Id = x.BlastocystScoreExpansionId.ToString(),
                blastocystScore_ICE_Id = x.BlastocystScoreIceId.ToString(),
                blastocystScore_TE_Id = x.BlastocystScoreTeId.ToString(),
                memo = x.Memo,
                kidScore = x.KidScore.ToString(),
                pgtaNumber = x.PgtaNumber.ToString(),
                pgtaResult = x.PgtaResult,
                pgtmResult = x.PgtmResult,
                operationTypeIds = x.ObservationNoteOperations.Where(y => y.IsDeleted == false).Select(y => y.OperationTypeId).ToList(),
                spindleResult = x.ObservationNoteOperations.Where(y => y.OperationTypeId == (int)OperationTypeEnum.Spindle && y.SpindleResult != null && y.IsDeleted == false).Select(y => y.SpindleResult).FirstOrDefault(),
                day = x.Day,
                observationNotePhotos = x.ObservationNotePhotos.Where(y => y.IsDeleted == false).Select(y => new ObservationNotePhotoDto
                {
                    observationNotePhotoId = y.ObservationNotePhotoId,
                    photoName = y.PhotoName,
                    isMainPhoto = y.IsMainPhoto
                }).ToList()
            }).AsNoTracking().FirstOrDefault();
        }

        public List<GetObservationNoteNameDto> GetObservationNoteNameByObservationNoteIds(List<Guid> observationNoteIds)
        {
            return _db.ObservationNotes.Where(x => observationNoteIds.Contains(x.ObservationNoteId)).Select(x => new GetObservationNoteNameDto
            {
                observationNoteId = x.ObservationNoteId,
                ovumDetailId = x.OvumDetailId,
                observationTime = x.ObservationTime,
                memo = x.Memo,
                kidScore = x.KidScore.ToString(),
                pgtaNumber = x.PgtaNumber.ToString(),
                pgtaResult = x.PgtaResult,
                pgtmResult = x.PgtmResult,
                day = x.Day,
                embryologist = x.EmbryologistNavigation.Name,
                ovumMaturationName = x.OvumMaturation.Name,
                observationTypeName = x.ObservationType.Name,
                fertilizationResultName = x.FertilizationResult.Name,
                blastomereScore_C_Name = x.BlastomereScoreC.Name,
                blastomereScore_G_Name = x.BlastomereScoreG.Name,
                blastomereScore_F_Name = x.BlastomereScoreF.Name,
                blastocystScore_Expansion_Name = x.BlastocystScoreExpansion.Name,
                blastocystScore_ICE_Name = x.BlastocystScoreIce.Name,
                blastocystScore_TE_Name = x.BlastocystScoreTe.Name,
                observationNotePhotos = x.ObservationNotePhotos.Where(y => y.IsDeleted == false).Select(y => new ObservationNotePhotoDto
                {
                    observationNotePhotoId = y.ObservationNotePhotoId,
                    photoName = y.PhotoName,
                    isMainPhoto = y.IsMainPhoto
                }).ToList()
            }).ToList();
        }
        public List<Common2Dto> GetObservationNoteOvumAbnormalityNameByObservationNoteIds(List<Guid> observationNoteIds)
        {
            return _db.ObservationNoteOvumAbnormalities.Where(x => observationNoteIds.Contains(x.ObservationNoteId) && !x.IsDeleted).Select(x => new Common2Dto
            {
                id = x.ObservationNoteId,
                name = x.OvumAbnormality.Name
            }).ToList();
        }
        public List<Common2Dto> GetObservationNoteEmbryoStatuseNameByObservationNoteIds(List<Guid> observationNoteIds)
        {
            return _db.ObservationNoteEmbryoStatuses.Where(x => observationNoteIds.Contains(x.ObservationNoteId) && !x.IsDeleted).Select(x => new Common2Dto
            {
                id = x.ObservationNoteId,
                name = x.EmbryoStatus.Name,
            }).ToList();
        }
        public List<ObservationNoteOperationDto> GetObservationNoteOperationNameByObservationNoteId(Guid observationNoteId)
        {
            return _db.ObservationNoteOperations.Where(x => x.ObservationNoteId == observationNoteId && x.IsDeleted == false).Select(x => new ObservationNoteOperationDto
            {
                operationTypeName = x.OperationType.Name,
                operationTypeId = x.OperationTypeId,
                spindleResult = x.SpindleResult
            }).ToList();
        }
        public List<GetObservationNoteNameDto> GetFreezeObservationNotes(List<Guid> ovumDetailIds)
        {
            return _db.ObservationNotes.Where(x => ovumDetailIds.Contains(x.OvumDetailId) && x.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation && !x.IsDeleted).Select(x => new GetObservationNoteNameDto
            {
                ovumNumber = x.OvumDetail.OvumNumber,
                day = x.Day,
                fertilizationResultName = x.FertilizationResult.Name,
                observationTime = x.ObservationTime,
                pgtaNumber = x.PgtaNumber.ToString(),
                pgtaResult = x.PgtaResult,
                kidScore = x.KidScore.ToString(),
                ovumMaturationName = x.OvumMaturation.Name,
                blastomereScore_C_Name = x.BlastomereScoreC.Name,
                blastomereScore_G_Name = x.BlastomereScoreG.Name,
                blastomereScore_F_Name = x.BlastomereScoreF.Name,
                blastocystScore_Expansion_Name = x.BlastocystScoreExpansion.Name,
                blastocystScore_ICE_Name = x.BlastocystScoreIce.Name,
                blastocystScore_TE_Name = x.BlastocystScoreTe.Name
            }).OrderBy(x => x.ovumNumber).AsNoTracking().ToList();

        }
        private ObservationNote GenerateObservationNote(ObservationNote observationNote, AddObservationNoteDto input)
        {
            observationNote.OvumDetailId = input.ovumDetailId;
            observationNote.Embryologist = input.embryologist;
            observationNote.Day = input.day;
            observationNote.IsDeleted = false;
            if (DateTime.TryParse(input.observationTime.ToString(), out DateTime nowTime))
            {
                observationNote.ObservationTime = nowTime;
            }
            else
            {
                throw new Exception("時間資訊有誤");
            }
            if (input.memo != "null")
            {
                observationNote.Memo = input.memo;
            }
            if (input.pgtaResult != "null")
            {
                observationNote.PgtaResult = input.pgtaResult;
            }
            if (input.pgtmResult != "null")
            {
                observationNote.PgtmResult = input.pgtmResult;
            }
            if (Int32.TryParse(input.pgtaNumber, out int pgtaNumber))
            {
                observationNote.PgtaNumber = pgtaNumber;
            }
            else
            {
                observationNote.PgtaNumber = null;
            }
            if (Int32.TryParse(input.ovumMaturationId, out int ovumMaturationId))
            {
                observationNote.OvumMaturationId = ovumMaturationId;
            }
            else
            {
                observationNote.OvumMaturationId = null;
            }
            if (Int32.TryParse(input.observationTypeId, out int observationTypeId))
            {
                observationNote.ObservationTypeId = observationTypeId;
            }
            else
            {
                observationNote.ObservationTypeId = null;
            }
            if (Int32.TryParse(input.fertilizationResultId, out int fertilizationResultId))
            {
                observationNote.FertilizationResultId = fertilizationResultId;
            }
            else
            {
                observationNote.FertilizationResultId = null;
            }
            if (Int32.TryParse(input.blastomereScore_C_Id, out int blastomereScore_C_Id))
            {
                observationNote.BlastomereScoreCId = blastomereScore_C_Id;
            }
            else
            {
                observationNote.BlastomereScoreCId = null;
            }
            if (Int32.TryParse(input.blastomereScore_G_Id, out int blastomereScore_G_Id))
            {
                observationNote.BlastomereScoreGId = blastomereScore_G_Id;
            }
            else
            {
                observationNote.BlastomereScoreGId = null;
            }
            if (Int32.TryParse(input.blastomereScore_F_Id, out int blastomereScore_F_Id))
            {
                observationNote.BlastomereScoreFId = blastomereScore_F_Id;
            }
            else
            {
                observationNote.BlastomereScoreFId = null;
            }

            if (Int32.TryParse(input.blastocystScore_Expansion_Id, out int blastocystScore_Expansion_Id))
            {
                observationNote.BlastocystScoreExpansionId = blastocystScore_Expansion_Id;
            }
            else
            {
                observationNote.BlastocystScoreExpansionId = null;
            }
            if (Int32.TryParse(input.blastocystScore_ICE_Id, out int blastocystScore_ICE_Id))
            {
                observationNote.BlastocystScoreIceId = blastocystScore_ICE_Id;
            }
            else
            {
                observationNote.BlastocystScoreIceId = null;
            }
            if (Int32.TryParse(input.blastocystScore_TE_Id, out int blastocystScore_TE_Id))
            {
                observationNote.BlastocystScoreTeId = blastocystScore_TE_Id;
            }
            else
            {
                observationNote.BlastocystScoreTeId = null;
            }

            if (decimal.TryParse(input.kidScore, out decimal kidScore))
            {
                if (kidScore >= 0 && kidScore <= Convert.ToDecimal(9.9))
                {
                    observationNote.KidScore = kidScore;
                }
                else
                {
                    throw new Exception("KID Score 數值需落在 0 - 9.9");
                }
            }
            else
            {
                observationNote.KidScore = null;
            }
            return observationNote;
        }
    }
}
