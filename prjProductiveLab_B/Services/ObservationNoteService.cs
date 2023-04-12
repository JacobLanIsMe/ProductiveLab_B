using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Enums;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;
using System.Text.Json;
using System.Transactions;

namespace prjProductiveLab_B.Services
{
    public class ObservationNoteService: IObservationNoteService
    {
        private readonly ReproductiveLabContext dbContext;
        private IWebHostEnvironment enviro;
        public ObservationNoteService(ReproductiveLabContext dbContext, IWebHostEnvironment enviro)
        {
            this.dbContext = dbContext;
            this.enviro = enviro;
        }

        public async Task<List<ObservationNoteDto>> GetObservationNote(Guid courseOfTreatmentId)
        {
            var result = await dbContext.OvumPickupDetails.Where(x => x.OvumPickup.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new ObservationNoteDto
            {
                ovumPickupDetailId = x.OvumPickupDetailId,
                ovumPickupDate = x.OvumPickup.UpdateTime,
                ovumNumber = x.OvumNumber,
                observationNote = dbContext.ObservationNotes.Where(y=>y.OvumPickupDetailId == x.OvumPickupDetailId && y.IsDeleted == false).Include(y=>y.ObservationNotePhotos).Select(y=>new Observation
                {
                    observationNoteId = y.ObservationNoteId,
                    observationType = y.ObservationType.Name,
                    day = y.Day,
                    observationTime = y.ObservationTime,
                    mainPhoto = y.ObservationNotePhotos.Where(z=>z.IsMainPhoto == true && z.IsDeleted == false).Select(z=>z.PhotoName).FirstOrDefault()
                }).ToList()
            }).OrderBy(x => x.ovumNumber).AsNoTracking().ToListAsync();
            foreach (var i in result)
            {
                foreach (var j in i.observationNote)
                {
                    if (j.mainPhoto != null)
                    {
                        string path = Path.Combine(enviro.ContentRootPath, "uploads", "images", j.mainPhoto);
                        if (File.Exists(path))
                        {
                            j.mainPhotoBase64 = Convert.ToBase64String(File.ReadAllBytes(path));
                        }
                    }
                }
                List<List<Observation>> observationList = new List<List<Observation>>();
                for (int j = 0; j < 7; j++)
                {
                    observationList.Add(i.observationNote.Where(x => x.day == j).OrderBy(x=>x.observationTime).ToList());
                }
                i.orderedObservationNote = observationList;
            }
            return result;
        }

        public async Task<List<CommonDto>> GetOvumMaturation()
        {
            return await dbContext.OvumMaturations.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
        public async Task<List<CommonDto>> GetObservationType()
        {
            return await dbContext.ObservationTypes.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
        public async Task<List<CommonDto>> GetOvumAbnormality()
        {
            return await dbContext.OvumAbnormalities.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
        public async Task<List<CommonDto>> GetFertilisationResult()
        {
            return await dbContext.FertilisationResults.Select(x=>new CommonDto
            {
                id=x.SqlId,
                name=x.Name
            }).OrderBy(x=>x.id).AsNoTracking().ToListAsync();
        }

        public async Task<BlastomereScoreDto> GetBlastomereScore()
        {
            var blastomereScore_C = await dbContext.BlastomereScoreCs.Select(x => new CommonDto
            {
                id = x.SlqId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
            var blastomereScore_G = await dbContext.BlastomereScoreGs.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
            var blastomereScore_F = await dbContext.BlastomereScoreFs.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
            BlastomereScoreDto result = new BlastomereScoreDto
            {
                blastomereScore_C = blastomereScore_C,
                blastomereScore_G = blastomereScore_G,
                blastomereScore_F = blastomereScore_F,
            };
            return result;
        }
        public async Task<List<CommonDto>> GetEmbryoStatus()
        {
            return await dbContext.EmbryoStatuses.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
        public async Task<BlastocystScoreDto> GetBlastocystScore()
        {
            var blastocystScore_Expansion = await dbContext.BlastocystScoreExpansions.Select(x=>new CommonDto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x=>x.id).AsNoTracking().ToListAsync();
            var blastocystScore_ICE = await dbContext.BlastocystScoreIces.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
            var blastocystScore_TE = await dbContext.BlastocystScoreTes.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
            BlastocystScoreDto result = new BlastocystScoreDto
            {
                blastocystScore_Expansion = blastocystScore_Expansion,
                blastocystScore_ICE = blastocystScore_ICE,
                blastocystScore_TE = blastocystScore_TE
            };
            return result;
        }

        public async Task<List<CommonDto>> GetOperationType()
        {
            return await dbContext.OperationTypes.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x => x.id).AsNoTracking().ToListAsync();
        }
        public async Task<BaseResponseDto> AddObservationNote(AddObservationNoteDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            ObservationNote observationNote = GenerateObservationNote(new ObservationNote(), input);
            
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    dbContext.ObservationNotes.Add(observationNote);
                    dbContext.SaveChanges();
                    Guid latestObservationNoteId = dbContext.ObservationNotes.OrderByDescending(x => x.SqlId).Select(x => x.ObservationNoteId).FirstOrDefault();
                    AddObservationNoteEmbryoStatus(latestObservationNoteId, input);
                    AddObservationNoteOvumAbnormality(latestObservationNoteId, input);
                    AddObservationNoteOperation(latestObservationNoteId, input);
                    AddObservationNotePhoto(input.photos, input.mainPhotoIndex, latestObservationNoteId, false);
                    scope.Complete();
                }
                result.SetSuccess();
            }
            catch (FormatException fex)
            {
                result.SetError(fex.Message);
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }

        

        private ObservationNote GenerateObservationNote(ObservationNote observationNote, AddObservationNoteDto input)
        {
            observationNote.OvumPickupDetailId = input.ovumPickupDetailId;
            observationNote.ObservationTime = input.observationTime;
            observationNote.Embryologist = input.embryologist;
            observationNote.Day = input.day;
            observationNote.IsDeleted = false;
            if (input.memo != "null")
            {
                observationNote.Memo = input.memo;
            }
            if (input.pgtaNumber != "null")
            {
                observationNote.Pgtanumber = input.pgtaNumber;
            }
            if (input.pgtaResult != "null")
            {
                observationNote.Pgtaresult = input.pgtaResult;
            }
            if (input.pgtmResult != "null")
            {
                observationNote.Pgtmresult = input.pgtmResult;
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
            if (Int32.TryParse(input.fertilisationResultId, out int fertilisationResultId))
            {
                observationNote.FertilisationResultId = fertilisationResultId;
            }
            else
            {
                observationNote.FertilisationResultId = null;
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
                    observationNote.Kidscore = kidScore;
                }
            }
            else
            {
                observationNote.Kidscore = null;
            }
            return observationNote;
        }
        private void deleteObservationNoteOvumAbnormality(Guid observationNoteId)
        {
            var q = dbContext.ObservationNoteOvumAbnormalities.Where(x => x.ObservationNoteId == observationNoteId);
            foreach (var i in q)
            {
                i.IsDeleted = true;
            }
            dbContext.SaveChanges();
        }
        private void AddObservationNoteOvumAbnormality(Guid observationNoteId, AddObservationNoteDto input)
        {
            try
            {
                List<int> ovumAbnormalityIds = JsonSerializer.Deserialize<List<int>>(input.ovumAbnormalityId);
                foreach (var i in ovumAbnormalityIds)
                {
                    ObservationNoteOvumAbnormality ovumAbnormality = new ObservationNoteOvumAbnormality
                    {
                        ObservationNoteId = observationNoteId,
                        ForeignKeyId = i,
                        IsDeleted = false
                    };
                    dbContext.ObservationNoteOvumAbnormalities.Add(ovumAbnormality);
                }
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void deleteObservationNoteOperation(Guid observationNoteId)
        {
            var q = dbContext.ObservationNoteOperations.Where(x => x.ObservationNoteId == observationNoteId);
            foreach (var i in q)
            {
                i.IsDeleted = true;
            }
            dbContext.SaveChanges();
        }
        private void AddObservationNoteOperation(Guid observationNoteId, AddObservationNoteDto input)
        {
            try
            {
                List<int> operationTypeIds = JsonSerializer.Deserialize<List<int>>(input.operationTypeId);
                foreach (var i in operationTypeIds)
                {
                    ObservationNoteOperation operation = new ObservationNoteOperation
                    {
                        ObservationNoteId = observationNoteId,
                        ForeignKeyId = i,
                        IsDeleted = false
                    };
                    if (i == (int)OperationTypeEnum.Spindle && input.spindleResult != "null")
                    {
                        operation.SpindleResult = input.spindleResult;
                    }
                    dbContext.ObservationNoteOperations.Add(operation);
                }
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        private void deleteObservationNoteEmbryoStatus(Guid observationNoteId)
        {
            var q = dbContext.ObservationNoteEmbryoStatuses.Where(x => x.ObservationNoteId == observationNoteId);
            foreach (var i in q)
            {
                i.IsDeleted = true;
            }
            dbContext.SaveChanges();
        }
        private void AddObservationNoteEmbryoStatus(Guid observationNoteId, AddObservationNoteDto input)
        {
            try
            {
                List<int> embryoStatusIds = JsonSerializer.Deserialize<List<int>>(input.embryoStatusId);
                foreach (var i in embryoStatusIds)
                {
                    ObservationNoteEmbryoStatus embryoStatus = new ObservationNoteEmbryoStatus
                    {
                        ObservationNoteId = observationNoteId,
                        ForeignKeyId = i,
                        IsDeleted = false
                    };
                    dbContext.ObservationNoteEmbryoStatuses.Add(embryoStatus);
                }
                dbContext.SaveChanges();
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
        }
        private void AddObservationNotePhoto(List<IFormFile>? photos, string inputMainPhotoIndex, Guid observationNoteId, bool hasAlreadyMainPhotoIndex)
        {
            if (photos != null)
            {
                int mainPhotoIndex = 0;
                if (!hasAlreadyMainPhotoIndex)
                {
                    if (!Int32.TryParse(inputMainPhotoIndex, out mainPhotoIndex))
                    {
                        throw new FormatException("主照片選項有誤");
                    }
                }
                for (int i = 0; i < photos.Count; i++)
                {
                    string pName = Guid.NewGuid() + ".png";
                    string path = Path.Combine(enviro.ContentRootPath, "uploads", "images", pName);
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
                    dbContext.ObservationNotePhotos.Add(photo);
                }
                dbContext.SaveChanges();
            }
        }

        
        public async Task<BaseResponseDto> UpdateObservationNote(UpdateObservationNoteDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            var existingObservationNote = dbContext.ObservationNotes.FirstOrDefault(x => x.ObservationNoteId == input.observationNoteId);
            if (existingObservationNote != null)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        AddObservationNoteDto inputTemp = (AddObservationNoteDto)input;
                        GenerateObservationNote(existingObservationNote, inputTemp);
                        dbContext.SaveChanges();
                        deleteObservationNoteEmbryoStatus(input.observationNoteId);
                        AddObservationNoteEmbryoStatus(input.observationNoteId, inputTemp);
                        deleteObservationNoteOperation(input.observationNoteId);
                        AddObservationNoteOperation(input.observationNoteId, inputTemp);
                        deleteObservationNoteOvumAbnormality(input.observationNoteId);
                        AddObservationNoteOvumAbnormality(input.observationNoteId, inputTemp);
                        var existingPhotos = dbContext.ObservationNotePhotos.Where(x => x.ObservationNoteId == input.observationNoteId && x.IsDeleted == false);
                        if (input.existingPhotos != null)
                        {           
                            List<ObservationNotePhotoDto> inputExistingPhotos = JsonSerializer.Deserialize<List<ObservationNotePhotoDto>>(input.existingPhotos);
                            
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
                            dbContext.SaveChanges();
                            var modifiedPhotos = dbContext.ObservationNotePhotos.Where(x => x.ObservationNoteId == input.observationNoteId && x.IsDeleted == false);
                            if (!modifiedPhotos.Any(x=>x.IsMainPhoto == true) && input.photos == null)
                            {
                                var q = modifiedPhotos.FirstOrDefault();
                                if (q != null)
                                {
                                    q.IsMainPhoto = true;
                                }
                            }
                            else if (!modifiedPhotos.Any(x => x.IsMainPhoto == true) && input.photos != null)
                            {
                                AddObservationNotePhoto(input.photos, input.mainPhotoIndex, input.observationNoteId, false);
                            }
                            else if (modifiedPhotos.Any(x => x.IsMainPhoto == true) && input.photos != null)
                            {
                                AddObservationNotePhoto(input.photos, input.mainPhotoIndex, input.observationNoteId, true);
                            }
                        }
                        else
                        {
                            foreach (var i in existingPhotos)
                            {
                                i.IsMainPhoto = false;
                                i.IsDeleted = true;
                            }
                            dbContext.SaveChanges();
                            AddObservationNotePhoto(input.photos, input.mainPhotoIndex, input.observationNoteId, false);
                        }
                        dbContext.SaveChanges();
                        scope.Complete();
                    }
                    result.SetSuccess();
                }
                catch(Exception ex)
                {
                    result.SetError(ex.Message);
                }

            }
            else
            {
                result.SetError("找不到此筆觀察紀錄");
            }
            return result;
        }

        public async Task<GetObservationNoteDto?> GetExistingObservationNote(Guid observationNoteId)
        {
            var result = await dbContext.ObservationNotes.Where(x => x.ObservationNoteId == observationNoteId).Include(x => x.ObservationNotePhotos).Include(x => x.ObservationNoteOperations).Include(x => x.ObservationNoteEmbryoStatuses).Include(x => x.ObservationNoteOvumAbnormalities).Select(x => new GetObservationNoteDto
            {
                ovumPickupDetailId = x.OvumPickupDetailId,
                observationTime = x.ObservationTime,
                embryologist = x.Embryologist,
                ovumMaturationId = x.OvumMaturationId.ToString(),
                observationTypeId = x.ObservationTypeId.ToString(),
                ovumAbnormalityIds = x.ObservationNoteOvumAbnormalities.Where(y => y.IsDeleted == false).Select(y => y.ForeignKeyId).ToList(),
                fertilisationResultId = x.FertilisationResultId.ToString(),
                blastomereScore_C_Id = x.BlastomereScoreCId.ToString(),
                blastomereScore_G_Id = x.BlastomereScoreGId.ToString(),
                blastomereScore_F_Id = x.BlastomereScoreFId.ToString(),
                embryoStatusIds = x.ObservationNoteEmbryoStatuses.Where(y => y.IsDeleted == false).Select(y => y.ForeignKeyId).ToList(),
                blastocystScore_Expansion_Id = x.BlastocystScoreExpansionId.ToString(),
                blastocystScore_ICE_Id = x.BlastocystScoreIceId.ToString(),
                blastocystScore_TE_Id = x.BlastocystScoreTeId.ToString(),
                memo = x.Memo,
                kidScore = x.Kidscore.ToString(),
                pgtaNumber = x.Pgtanumber,
                pgtaResult = x.Pgtaresult,
                pgtmResult = x.Pgtmresult,
                operationTypeIds = x.ObservationNoteOperations.Where(y => y.IsDeleted == false).Select(y => y.ForeignKeyId).ToList(),
                spindleResult = x.ObservationNoteOperations.Where(y => y.ForeignKeyId == (int)OperationTypeEnum.Spindle && y.SpindleResult != null && y.IsDeleted == false).Select(y => y.SpindleResult).FirstOrDefault(),
                day = x.Day,
                observationNotePhotos = x.ObservationNotePhotos.Where(y => y.IsDeleted == false).Select(y => new ObservationNotePhotoDto
                {
                    observationNotePhotoId = y.ObservationNotePhotoId,
                    photoName = y.PhotoName,
                    isMainPhoto = y.IsMainPhoto
                }).ToList()
            }).AsNoTracking().FirstOrDefaultAsync();
            if (result == null)
            {
                return null;
            }
            else
            {
                result.ovumAbnormalityId = JsonSerializer.Serialize(result.ovumAbnormalityIds);
                result.embryoStatusId = JsonSerializer.Serialize(result.embryoStatusIds);
                result.operationTypeId = JsonSerializer.Serialize(result.operationTypeIds);
                if (result.observationNotePhotos != null && result.observationNotePhotos.Count != 0)
                {
                    GetObservationNotePhotoBase64String(result.observationNotePhotos);
                }
                return result;
            }

        }
        public async Task<GetObservationNoteNameDto?> GetExistingObservationNoteName(Guid observationNoteId)
        {
            var result = await dbContext.ObservationNotes.Where(x => x.ObservationNoteId == observationNoteId).Include(x => x.ObservationNotePhotos).Select(x => new GetObservationNoteNameDto
            {
                ovumPickupDetailId = x.OvumPickupDetailId,
                observationTime = x.ObservationTime,
                memo = x.Memo,
                kidScore = x.Kidscore.ToString(),
                pgtaNumber = x.Pgtanumber,
                pgtaResult = x.Pgtaresult,
                pgtmResult = x.Pgtmresult,
                day = x.Day,
                embryologist = x.EmbryologistNavigation.Name,
                ovumMaturationName = x.OvumMaturation.Name,
                observationTypeName = x.ObservationType.Name,
                ovumAbnormalityName = dbContext.ObservationNoteOvumAbnormalities.Where(y=>y.ObservationNoteId == observationNoteId && y.IsDeleted == false).Select(y=>y.ForeignKey.Name).ToList(),
                fertilisationResultName = x.FertilisationResult.Name,
                blastomereScore_C_Name = x.BlastomereScoreC.Name,
                blastomereScore_G_Name = x.BlastomereScoreG.Name,
                blastomereScore_F_Name = x.BlastomereScoreF.Name,
                embryoStatusName = dbContext.ObservationNoteEmbryoStatuses.Where(y=>y.ObservationNoteId == observationNoteId && y.IsDeleted == false).Select(y=>y.ForeignKey.Name).ToList(),
                blastocystScore_Expansion_Name = x.BlastocystScoreExpansion.Name,
                blastocystScore_ICE_Name = x.BlastocystScoreIce.Name,
                blastocystScore_TE_Name = x.BlastocystScoreTe.Name,
                observationNotePhotos = x.ObservationNotePhotos.Where(y=>y.IsDeleted == false).Select(y => new ObservationNotePhotoDto
                {
                    observationNotePhotoId = y.ObservationNotePhotoId,
                    photoName = y.PhotoName,
                    isMainPhoto = y.IsMainPhoto
                }).ToList()
            }).AsNoTracking().FirstOrDefaultAsync();
            if (result == null)
            {
                return null;
            }
            else
            {
                var q = dbContext.ObservationNoteOperations.Where(x => x.ObservationNoteId == observationNoteId && x.IsDeleted == false).Select(x => new
                {
                    operationTypeName = x.ForeignKey.Name,
                    foreignKeyId = x.ForeignKeyId,
                    spindleResult = x.SpindleResult
                }).ToList();
                result.operationTypeName = q.Select(x => x.operationTypeName).ToList();
                result.spindleResult = q.Where(x => x.foreignKeyId == (int)OperationTypeEnum.Spindle && x.spindleResult != null).Select(x => x.spindleResult).FirstOrDefault();
                if (result.observationNotePhotos != null && result.observationNotePhotos.Count != 0)
                {
                    GetObservationNotePhotoBase64String(result.observationNotePhotos);
                }
                return result;
            }
        }
        private void GetObservationNotePhotoBase64String(List<ObservationNotePhotoDto> observationNotePhotos)
        {
            foreach (var i in observationNotePhotos)
            {
                string path = Path.Combine(enviro.ContentRootPath, "uploads", "images", i.photoName);
                if (File.Exists(path))
                {
                    i.imageBase64String = Convert.ToBase64String(File.ReadAllBytes(path));
                }
            }
        }

        public async Task<BaseResponseDto> DeleteObservationNote(Guid observationNoteId)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var observationNote = dbContext.ObservationNotes.Where(x => x.ObservationNoteId == observationNoteId).FirstOrDefault();
                    if (observationNote == null)
                    {
                        throw new Exception("找不到此筆觀察紀錄");
                    }
                    else if (observationNote.IsDeleted == true)
                    {
                        throw new Exception("此筆觀察紀錄異常");
                    }
                    else
                    {
                        observationNote.IsDeleted = true;
                    }
                    var observationNotePhotos = dbContext.ObservationNotePhotos.Where(x => x.ObservationNoteId == observationNoteId).ToList();
                    foreach (var i in observationNotePhotos)
                    {
                        i.IsDeleted = true;
                    }
                    dbContext.SaveChanges();
                    deleteObservationNoteOperation(observationNoteId);
                    deleteObservationNoteOvumAbnormality(observationNoteId);
                    deleteObservationNoteEmbryoStatus(observationNoteId);
                    scope.Complete();
                }
                result.SetSuccess();
            }
            catch(Exception ex)
            {
                result.SetError(ex.Message);
            }
            
            return result;
        }
    }
}
