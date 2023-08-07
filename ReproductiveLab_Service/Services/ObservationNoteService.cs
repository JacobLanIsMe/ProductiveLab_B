using Reproductive_SharedFunction.Interfaces;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Service.Interfaces;
using System.Text.Json;
using System.Transactions;

namespace ReproductiveLab_Service.Services
{
    public class ObservationNoteService : IObservationNoteService
    {
        private readonly IObservationNoteRepository _observationNoteRepository;
        //private readonly IPhotoFunction _photoFunction;
        private readonly IObservationNoteFunction _observationNoteFunction;
        public ObservationNoteService(IObservationNoteRepository observationNoteRepository,/* IPhotoFunction photoFunction,*/ IObservationNoteFunction observationNoteFunction)
        {
            _observationNoteRepository = observationNoteRepository;
            //_photoFunction = photoFunction;
            _observationNoteFunction = observationNoteFunction;
        }
        public List<ObservationNoteDto> GetObservationNote(Guid courseOfTreatmentId)
        {
            var result = _observationNoteRepository.GetObservationNoteByCourseOfTreatmentId(courseOfTreatmentId);
            foreach (var i in result)
            {
                //foreach (var j in i.observationNote)
                //{
                //    if (j.mainPhoto != null)
                //    {
                //        j.mainPhotoBase64 = _photoFunction.GetBase64String(j.mainPhoto);
                //    }
                //}
                List<List<Observation>> observationList = new List<List<Observation>>();
                for (int j = 0; j < 7; j++)
                {
                    observationList.Add(i.observationNote.Where(x => x.day == j).OrderBy(x => x.observationTime).ToList());
                }
                i.orderedObservationNote = observationList;
            }
            return result;
        }
        public List<Common1Dto> GetOvumMaturation()
        {
            return _observationNoteRepository.GetOvumMaturation();
        }
        public List<Common1Dto> GetObservationType()
        {
            return _observationNoteRepository.GetObservationType();
        }
        public List<Common1Dto> GetOvumAbnormality()
        {
            return _observationNoteRepository.GetOvumAbnormality();
        }
        public List<Common1Dto> GetFertilizationResult()
        {
            return _observationNoteRepository.GetFertilizationResult();
        }
        public BlastomereScoreDto GetBlastomereScore()
        {
            var blastomereScore_C = _observationNoteRepository.GetBlastomereScoreC();
            var blastomereScore_G = _observationNoteRepository.GetBlastomereScoreG();
            var blastomereScore_F = _observationNoteRepository.GetBlastomereScoreF();
            BlastomereScoreDto result = new BlastomereScoreDto
            {
                blastomereScore_C = blastomereScore_C,
                blastomereScore_G = blastomereScore_G,
                blastomereScore_F = blastomereScore_F,
            };
            return result;
        }
        public List<Common1Dto> GetEmbryoStatus()
        {
            return _observationNoteRepository.GetEmbryoStatus();
        }
        public BlastocystScoreDto GetBlastocystScore()
        {
            var blastocystScore_Expansion = _observationNoteRepository.GetBlastocystScoreExpansion();
            var blastocystScore_ICE = _observationNoteRepository.GetBlastocystScoreIce();
            var blastocystScore_TE = _observationNoteRepository.GetBlastocystScoreTe();
            BlastocystScoreDto result = new BlastocystScoreDto
            {
                blastocystScore_Expansion = blastocystScore_Expansion,
                blastocystScore_ICE = blastocystScore_ICE,
                blastocystScore_TE = blastocystScore_TE
            };
            return result;
        }
        public List<Common1Dto> GetOperationType()
        {
            return _observationNoteRepository.GetOperationType();
        }
        public BaseResponseDto AddObservationNote(AddObservationNoteDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _observationNoteRepository.AddObservationNote(input);
                    Guid latestObservationNoteId = _observationNoteRepository.GetLatestObservationNoteId();
                    _observationNoteFunction.AddObservationNotePhoto(input.photos, input.mainPhotoIndex, latestObservationNoteId, false);
                    _observationNoteFunction.AddObservationNoteEmbryoStatus(latestObservationNoteId, input);
                    _observationNoteFunction.AddObservationNoteOvumAbnormality(latestObservationNoteId, input);
                    _observationNoteFunction.AddObservationNoteOperation(latestObservationNoteId, input);
                    
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

        public BaseResponseDto UpdateObservationNote(UpdateObservationNoteDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            var existingObservationNote = _observationNoteRepository.GetObservationNoteById(input.observationNoteId);
            if (existingObservationNote != null)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        AddObservationNoteDto inputTemp = (AddObservationNoteDto)input;
                        _observationNoteRepository.UpdateObservationNote(existingObservationNote, inputTemp);
                        _observationNoteFunction.DeleteObservationNoteEmbryoStatus(input.observationNoteId);
                        _observationNoteFunction.AddObservationNoteEmbryoStatus(input.observationNoteId, inputTemp);
                        _observationNoteFunction.DeleteObservationNoteOperation(input.observationNoteId);
                        _observationNoteFunction.AddObservationNoteOperation(input.observationNoteId, inputTemp);
                        _observationNoteFunction.DeleteObservationNoteOvumAbnormality(input.observationNoteId);
                        _observationNoteFunction.AddObservationNoteOvumAbnormality(input.observationNoteId, inputTemp);
                        var existingPhotos = _observationNoteRepository.GetObservatioNotePhotosByObservationNoteId(input.observationNoteId);
                        if (input.existingPhotos != null)
                        {
                            List<ObservationNotePhotoDto> inputExistingPhotos = JsonSerializer.Deserialize<List<ObservationNotePhotoDto>>(input.existingPhotos);
                            _observationNoteRepository.UpdateObservationNotePhoto(existingPhotos, inputExistingPhotos);
                            var modifiedPhotos = _observationNoteRepository.GetObservatioNotePhotosByObservationNoteId(input.observationNoteId);
                            if (!modifiedPhotos.Any(x => x.IsMainPhoto == true) && input.photos == null)
                            {
                                var q = modifiedPhotos.FirstOrDefault();
                                if (q != null)
                                {
                                    q.IsMainPhoto = true;
                                }
                            }
                            else if (!modifiedPhotos.Any(x => x.IsMainPhoto == true) && input.photos != null)
                            {
                                _observationNoteFunction.AddObservationNotePhoto(input.photos, input.mainPhotoIndex, input.observationNoteId, false);
                            }
                            else if (modifiedPhotos.Any(x => x.IsMainPhoto == true) && input.photos != null)
                            {
                                _observationNoteFunction.AddObservationNotePhoto(input.photos, input.mainPhotoIndex, input.observationNoteId, true);
                            }
                        }
                        else
                        {
                            _observationNoteRepository.DeleteObservationNotePhoto(existingPhotos);
                            _observationNoteFunction.AddObservationNotePhoto(input.photos, input.mainPhotoIndex, input.observationNoteId, false);
                        }
                        scope.Complete();
                    }
                    result.SetSuccess();
                }
                catch (Exception ex)
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
        public GetObservationNoteDto? GetExistingObservationNote(Guid observationNoteId)
        {
            var result = _observationNoteRepository.GetExistingObservationNote(observationNoteId);
            if (result == null)
            {
                return null;
            }
            else
            {
                result.ovumAbnormalityId = JsonSerializer.Serialize(result.ovumAbnormalityIds);
                result.embryoStatusId = JsonSerializer.Serialize(result.embryoStatusIds);
                result.operationTypeId = JsonSerializer.Serialize(result.operationTypeIds);
                return result;
            }

        }

        public List<GetObservationNoteNameDto> GetObservationNoteNameByObservationNoteIds(List<Guid> observationNoteIds)
        {
            var result = _observationNoteRepository.GetObservationNoteNameByObservationNoteIds(observationNoteIds);
            var ovumAbnormalityNames = _observationNoteRepository.GetObservationNoteOvumAbnormalityNameByObservationNoteIds(observationNoteIds);
            var embryoStatusNames = _observationNoteRepository.GetObservationNoteEmbryoStatuseNameByObservationNoteIds(observationNoteIds);
            foreach (var i in result)
            {
                i.ovumAbnormalityName = ovumAbnormalityNames.Where(x => x.id == i.observationNoteId).Select(x => x.name).ToList();
                i.embryoStatusName = embryoStatusNames.Where(x => x.id == i.observationNoteId).Select(x => x.name).ToList();
            }
            return result;
        }
        public GetObservationNoteNameDto? GetExistingObservationNoteName(Guid observationNoteId)
        {
            List<Guid> observationNoteIds = new List<Guid> { observationNoteId };
            List<GetObservationNoteNameDto> observationNotes = GetObservationNoteNameByObservationNoteIds(observationNoteIds);
            if (observationNotes.Count != 1)
            {
                return null;
            }
            List<ObservationNoteOperationDto> observationNoteOperations = _observationNoteRepository.GetObservationNoteOperationNameByObservationNoteId(observationNoteId);
            observationNotes[0].operationTypeName = observationNoteOperations.Select(x => x.operationTypeName).ToList();
            observationNotes[0].spindleResult = observationNoteOperations.Where(x => x.operationTypeId == (int)OperationTypeEnum.Spindle && x.spindleResult != null).Select(x => x.spindleResult).FirstOrDefault();
            return observationNotes[0];
        }
        public BaseResponseDto DeleteObservationNote(Guid observationNoteId)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var observationNote = _observationNoteRepository.GetObservationNoteById(observationNoteId);
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
                    _observationNoteRepository.DeleteObservationNotePhoto(observationNoteId);
                    _observationNoteFunction.DeleteObservationNoteOperation(observationNoteId);
                    _observationNoteFunction.DeleteObservationNoteOvumAbnormality(observationNoteId);
                    _observationNoteFunction.DeleteObservationNoteEmbryoStatus(observationNoteId);
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

        public List<GetObservationNoteNameDto> GetFreezeObservationNotes(List<Guid> ovumDetailIds)
        {
            return _observationNoteRepository.GetFreezeObservationNotes(ovumDetailIds);

        }

        //private ObservationNote GenerateObservationNote(ObservationNote observationNote, AddObservationNoteDto input)
        //{
        //    observationNote.OvumDetailId = input.ovumDetailId;
        //    observationNote.Embryologist = input.embryologist;
        //    observationNote.Day = input.day;
        //    observationNote.IsDeleted = false;
        //    if (DateTime.TryParse(input.observationTime.ToString(), out DateTime nowTime))
        //    {
        //        observationNote.ObservationTime = nowTime;
        //    }
        //    else
        //    {
        //        throw new Exception("時間資訊有誤");
        //    }
        //    if (input.memo != "null")
        //    {
        //        observationNote.Memo = input.memo;
        //    }
        //    if (input.pgtaResult != "null")
        //    {
        //        observationNote.Pgtaresult = input.pgtaResult;
        //    }
        //    if (input.pgtmResult != "null")
        //    {
        //        observationNote.Pgtmresult = input.pgtmResult;
        //    }
        //    if (Int32.TryParse(input.pgtaNumber, out int pgtaNumber))
        //    {
        //        observationNote.Pgtanumber = pgtaNumber;
        //    }
        //    else
        //    {
        //        observationNote.Pgtanumber = null;
        //    }
        //    if (Int32.TryParse(input.ovumMaturationId, out int ovumMaturationId))
        //    {
        //        observationNote.OvumMaturationId = ovumMaturationId;
        //    }
        //    else
        //    {
        //        observationNote.OvumMaturationId = null;
        //    }
        //    if (Int32.TryParse(input.observationTypeId, out int observationTypeId))
        //    {
        //        observationNote.ObservationTypeId = observationTypeId;
        //    }
        //    else
        //    {
        //        observationNote.ObservationTypeId = null;
        //    }
        //    if (Int32.TryParse(input.fertilizationResultId, out int fertilizationResultId))
        //    {
        //        observationNote.FertilizationResultId = fertilizationResultId;
        //    }
        //    else
        //    {
        //        observationNote.FertilizationResultId = null;
        //    }
        //    if (Int32.TryParse(input.blastomereScore_C_Id, out int blastomereScore_C_Id))
        //    {
        //        observationNote.BlastomereScoreCId = blastomereScore_C_Id;
        //    }
        //    else
        //    {
        //        observationNote.BlastomereScoreCId = null;
        //    }
        //    if (Int32.TryParse(input.blastomereScore_G_Id, out int blastomereScore_G_Id))
        //    {
        //        observationNote.BlastomereScoreGId = blastomereScore_G_Id;
        //    }
        //    else
        //    {
        //        observationNote.BlastomereScoreGId = null;
        //    }
        //    if (Int32.TryParse(input.blastomereScore_F_Id, out int blastomereScore_F_Id))
        //    {
        //        observationNote.BlastomereScoreFId = blastomereScore_F_Id;
        //    }
        //    else
        //    {
        //        observationNote.BlastomereScoreFId = null;
        //    }

        //    if (Int32.TryParse(input.blastocystScore_Expansion_Id, out int blastocystScore_Expansion_Id))
        //    {
        //        observationNote.BlastocystScoreExpansionId = blastocystScore_Expansion_Id;
        //    }
        //    else
        //    {
        //        observationNote.BlastocystScoreExpansionId = null;
        //    }
        //    if (Int32.TryParse(input.blastocystScore_ICE_Id, out int blastocystScore_ICE_Id))
        //    {
        //        observationNote.BlastocystScoreIceId = blastocystScore_ICE_Id;
        //    }
        //    else
        //    {
        //        observationNote.BlastocystScoreIceId = null;
        //    }
        //    if (Int32.TryParse(input.blastocystScore_TE_Id, out int blastocystScore_TE_Id))
        //    {
        //        observationNote.BlastocystScoreTeId = blastocystScore_TE_Id;
        //    }
        //    else
        //    {
        //        observationNote.BlastocystScoreTeId = null;
        //    }

        //    if (decimal.TryParse(input.kidScore, out decimal kidScore))
        //    {
        //        if (kidScore >= 0 && kidScore <= Convert.ToDecimal(9.9))
        //        {
        //            observationNote.Kidscore = kidScore;
        //        }
        //        else
        //        {
        //            throw new Exception("KID Score 數值需落在 0 - 9.9");
        //        }
        //    }
        //    else
        //    {
        //        observationNote.Kidscore = null;
        //    }
        //    return observationNote;
        //}
        //private void AddObservationNoteEmbryoStatus(Guid observationNoteId, AddObservationNoteDto input)
        //{
        //    List<int> embryoStatusIds = JsonSerializer.Deserialize<List<int>>(input.embryoStatusId);
        //    _observationNoteRepository.AddObservationNoteEmbryoStatus(observationNoteId, embryoStatusIds);
        //}
        //private void AddObservationNoteOvumAbnormality(Guid observationNoteId, AddObservationNoteDto input)
        //{
        //    List<int> ovumAbnormalityIds = JsonSerializer.Deserialize<List<int>>(input.ovumAbnormalityId);
        //    _observationNoteRepository.AddObservationNoteOvumAbnormality(observationNoteId, ovumAbnormalityIds);
        //}
        //private void AddObservationNoteOperation(Guid observationNoteId, AddObservationNoteDto input)
        //{
        //    List<int> operationTypeIds = JsonSerializer.Deserialize<List<int>>(input.operationTypeId);
        //    _observationNoteRepository.AddObservationNoteOperation(observationNoteId, input, operationTypeIds);
        //}
        //private void AddObservationNotePhoto(List<IFormFile>? photos, string inputMainPhotoIndex, Guid observationNoteId, bool hasAlreadyMainPhotoIndex)
        //{
        //    if (photos != null)
        //    {
        //        int mainPhotoIndex = 0;
        //        if (!hasAlreadyMainPhotoIndex)
        //        {
        //            if (!Int32.TryParse(inputMainPhotoIndex, out mainPhotoIndex))
        //            {
        //                throw new FormatException("主照片選項有誤");
        //            }
        //        }
        //        _observationNoteRepository.AddObservationNotePhoto(photos, observationNoteId, hasAlreadyMainPhotoIndex, mainPhotoIndex);
        //    }
        //}
        //private void deleteObservationNoteEmbryoStatus(Guid observationNoteId)
        //{
        //    _observationNoteRepository.deleteObservationNoteEmbryoStatus(observationNoteId);
        //}
        //private void deleteObservationNoteOperation(Guid observationNoteId)
        //{
        //    _observationNoteRepository.deleteObservationNoteOperation(observationNoteId);
        //}
        //private void deleteObservationNoteOvumAbnormality(Guid observationNoteId)
        //{
        //    _observationNoteRepository.deleteObservationNoteOvumAbnormality(observationNoteId);
        //}
    }
}
