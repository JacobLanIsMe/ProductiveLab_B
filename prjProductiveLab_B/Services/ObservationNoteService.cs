using Microsoft.EntityFrameworkCore;
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
            ObservationNote observationNote = new ObservationNote
            {
                OvumPickupDetailId = input.ovumPickupDetailId,
                ObservationTime = input.observationTime,
                Embryologist = input.embryologist,
                Memo = input.memo,
                Kidscore = input.kidScore,
                Pgtanumber = input.pgtaNumber,
                Pgtaresult = input.pgtaResult,
                Pgtmresult = input.pgtmResult,
                Day = input.day,
                IsDeleted = false
            };
            try
            {
                if (input.ovumMaturationId != null)
                {
                    observationNote.OvumMaturationId = Convert.ToInt32(input.ovumMaturationId);
                }
                if (input.observationTypeId != null)
                {
                    observationNote.ObservationTypeId = Convert.ToInt32(input.observationTypeId);
                }
                if (input.ovumAbnormalityId != null)
                {
                    observationNote.OvumAbnormalityId = Convert.ToInt32(input.ovumAbnormalityId);
                }
                if (input.fertilisationResultId != null)
                {
                    observationNote.FertilisationResultId = Convert.ToInt32(input.fertilisationResultId);
                }
                if (input.blastomereScore_C_Id != null)
                {
                    observationNote.BlastomereScoreCId = Convert.ToInt32(input.blastomereScore_C_Id);
                }
                if (input.blastomereScore_G_Id != null)
                {
                    observationNote.BlastomereScoreGId = Convert.ToInt32(input.blastomereScore_G_Id);
                }
                if (input.blastomereScore_F_Id != null)
                {
                    observationNote.BlastomereScoreFId = Convert.ToInt32(input.blastomereScore_F_Id);
                }
                if (input.embryoStatusId != null)
                {
                    observationNote.EmbryoStatusId = Convert.ToInt32(input.embryoStatusId);
                }
                if (input.blastocystScore_Expansion_Id != null)
                {
                    observationNote.BlastocystScoreExpansionId = Convert.ToInt32(input.blastocystScore_Expansion_Id);
                }
                if (input.blastocystScore_ICE_Id != null)
                {
                    observationNote.BlastocystScoreIceId = Convert.ToInt32(input.blastocystScore_ICE_Id);
                }
                if (input.blastocystScore_TE_Id != null)
                {
                    observationNote.BlastocystScoreTeId = Convert.ToInt32(input.blastocystScore_TE_Id);
                }
                if (input.operationTypeId != null)
                {
                    observationNote.OperationTypeId = Convert.ToInt32(input.operationTypeId);
                }
            }
            catch (FormatException fex)
            {
                result.SetError(fex.Message);
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
            }
            
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
                        if (input.mainPhotoIndex!= null)
                        {
                            try
                            {
                                mainPhotoIndex = Convert.ToInt32(input.mainPhotoIndex);
                            }
                            catch(FormatException fex)
                            {
                                result.SetError(fex.Message);
                            }
                            catch(Exception ex)
                            {
                                result.SetError(ex.Message);
                            }
                        }
                        for (int i = 0;  i < input.photos.Count; i++)
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
                                IsMainPhoto = mainPhotoIndex == i ? true:false,
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
            catch (Exception ex)
            {
                result.SetError(ex.Message);
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
                kidScore = x.Kidscore,
                pgtaNumber = x.Pgtanumber,
                pgtaResult = x.Pgtaresult,
                pgtmResult = x.Pgtmresult,
                operationTypeId = x.OperationTypeId.ToString(),
                day = x.Day,
                observationNotePhotos = x.ObservationNotePhotos.Select(y=>new observationNotePhotoDto
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
                    foreach (var i in result.observationNotePhotos)
                    {
                        string path = Path.Combine(enviro.ContentRootPath, "uploads", "images", i.route);
                        if (File.Exists(path))
                        {
                            i.imageBase64String = Convert.ToBase64String(File.ReadAllBytes(path));
                        }
                    }
                }
                return result;
            }
            
        }
    }
}
