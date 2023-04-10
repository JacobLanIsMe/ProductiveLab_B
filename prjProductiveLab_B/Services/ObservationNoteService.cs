﻿using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;
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
                    mainPhoto = y.ObservationNotePhotos.Where(z=>z.IsMainPhoto == true && z.IsDeleted == false).Select(z=>z.Route).FirstOrDefault()
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
                    if (input.photos != null)
                    {
                        int mainPhotoIndex = 0;
                        if (!Int32.TryParse(input.mainPhotoIndex, out mainPhotoIndex))
                        {
                            throw new FormatException("主照片選項有誤");
                        }
                        for (int i = 0; i < input.photos.Count; i++)
                        {
                            string pName = Guid.NewGuid() + ".png";
                            string path = Path.Combine(enviro.ContentRootPath, "uploads", "images", pName);
                            using (FileStream stream = new FileStream(path, FileMode.Create))
                            {
                                input.photos[i].CopyTo(stream);
                            }
                            ObservationNotePhoto photo = new ObservationNotePhoto
                            {
                                ObservationNoteId = latestObservationNoteId,
                                Route = pName,
                                IsMainPhoto = mainPhotoIndex == i ? true : false,
                                IsDeleted = false
                            };
                            dbContext.ObservationNotePhotos.Add(photo);
                        }
                    }
                    dbContext.SaveChanges();
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
            if (Int32.TryParse(input.ovumAbnormalityId, out int ovumAbnormalityId))
            {
                observationNote.OvumAbnormalityId = ovumAbnormalityId;
            }
            else
            {
                observationNote.OvumAbnormalityId = null;
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
            if (Int32.TryParse(input.embryoStatusId, out int embryoStatusId))
            {
                observationNote.EmbryoStatusId = embryoStatusId;
            }
            else
            {
                observationNote.EmbryoStatusId = null;
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
            if (Int32.TryParse(input.operationTypeId, out int operationTypeId))
            {
                observationNote.OperationTypeId = operationTypeId;
            }
            else
            {
                observationNote.OperationTypeId = null;
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

        public async Task<BaseResponseDto> UpdateObservationNote(UpdateObservationNoteDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            var existingObservationNote = dbContext.ObservationNotes.FirstOrDefault(x => x.ObservationNoteId == input.observationNoteId);
            if (existingObservationNote != null)
            {
                AddObservationNoteDto inputTemp = (AddObservationNoteDto)input;
                GenerateObservationNote(existingObservationNote, inputTemp);
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
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
            var result = await dbContext.ObservationNotes.Where(x=>x.ObservationNoteId== observationNoteId).Include(x=>x.ObservationNotePhotos).Select(x=>new GetObservationNoteDto
            {
                ovumPickupDetailId = x.OvumPickupDetailId,
                observationTime= x.ObservationTime,
                embryologist = x.Embryologist,
                ovumMaturationId = x.OvumMaturationId.ToString(),
                observationTypeId = x.ObservationTypeId.ToString(),
                ovumAbnormalityId = x.OvumAbnormalityId.ToString(),
                fertilisationResultId = x.FertilisationResultId.ToString(),
                blastomereScore_C_Id = x.BlastomereScoreCId.ToString(),
                blastomereScore_G_Id = x.BlastomereScoreGId.ToString(),
                blastomereScore_F_Id = x.BlastomereScoreFId.ToString(),
                embryoStatusId = x.EmbryoStatusId.ToString(),
                blastocystScore_Expansion_Id = x.BlastocystScoreExpansionId.ToString(),
                blastocystScore_ICE_Id = x.BlastocystScoreIceId.ToString(),
                blastocystScore_TE_Id = x.BlastocystScoreTeId.ToString(),
                memo = x.Memo,
                kidScore = x.Kidscore.ToString(),
                pgtaNumber = x.Pgtanumber,
                pgtaResult = x.Pgtaresult,
                pgtmResult = x.Pgtmresult,
                operationTypeId = x.OperationTypeId.ToString(),
                day = x.Day,
                observationNotePhotos = x.ObservationNotePhotos.Select(y=>new ObservationNotePhotoDto
                {
                    observationNotePhotoId = y.ObservationNotePhotoId,
                    route = y.Route,
                    isMainPhoto = y.IsMainPhoto
                }).ToList()
            }).AsNoTracking().FirstOrDefaultAsync();
            if (result == null)
            {
                return null;
            }
            else
            {
                if (result.observationNotePhotos!=null && result.observationNotePhotos.Count != 0)
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
                ovumAbnormalityName = x.OvumAbnormality.Name,
                fertilisationResultName = x.FertilisationResult.Name,
                blastomereScore_C_Name = x.BlastomereScoreC.Name,
                blastomereScore_G_Name = x.BlastomereScoreG.Name,
                blastomereScore_F_Name = x.BlastomereScoreF.Name,
                embryoStatusName = x.EmbryoStatus.Name,
                blastocystScore_Expansion_Name = x.BlastocystScoreExpansion.Name,
                blastocystScore_ICE_Name = x.BlastocystScoreIce.Name,
                blastocystScore_TE_Name = x.BlastocystScoreTe.Name,
                operationTypeName = x.OperationType.Name,
                observationNotePhotos = x.ObservationNotePhotos.Select(y => new ObservationNotePhotoDto
                {
                    observationNotePhotoId = y.ObservationNotePhotoId,
                    route = y.Route,
                    isMainPhoto = y.IsMainPhoto
                }).ToList()
            }).AsNoTracking().FirstOrDefaultAsync();
            if (result == null)
            {
                return null;
            }
            else
            {
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
                string path = Path.Combine(enviro.ContentRootPath, "uploads", "images", i.route);
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
                        if (i.IsDeleted == true)
                        {
                            throw new Exception("此筆觀察紀錄的圖片狀態異常");
                        }
                        else
                        {
                            i.IsDeleted = true;
                        }
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
            
            return result;
        }
    }
}
