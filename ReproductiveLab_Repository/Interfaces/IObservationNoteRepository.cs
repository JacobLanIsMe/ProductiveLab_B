using Microsoft.AspNetCore.Http;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface IObservationNoteRepository
    {
        IQueryable<ObservationNote> GetObservationNotesByOvumDetailIds(List<Guid> ovumDetailIds);
        List<ObservationNoteDto> GetObservationNoteByCourseOfTreatmentId(Guid courseOfTreatmentId);
        List<Common1Dto> GetOvumMaturation();
        List<Common1Dto> GetObservationType();
        List<Common1Dto> GetOvumAbnormality();
        List<Common1Dto> GetFertilizationResult();
        List<Common1Dto> GetBlastomereScoreC();
        List<Common1Dto> GetBlastomereScoreG();
        List<Common1Dto> GetBlastomereScoreF();
        List<Common1Dto> GetEmbryoStatus();
        List<Common1Dto> GetBlastocystScoreExpansion();
        List<Common1Dto> GetBlastocystScoreIce();
        List<Common1Dto> GetBlastocystScoreTe();
        List<Common1Dto> GetOperationType();
        void AddObservationNote(AddObservationNoteDto input);
        void UpdateObservationNote(ObservationNote existingObservationNote, AddObservationNoteDto input);
        Guid GetLatestObservationNoteId();
        void AddObservationNoteEmbryoStatus(Guid observationNoteId, List<int> embryoStatusIds);
        void AddObservationNoteOvumAbnormality(Guid observationNoteId, List<int> ovumAbnormalityIds);
        void AddObservationNoteOperation(Guid observationNoteId, AddObservationNoteDto input, List<int> operationTypeIds);
        Task AddObservationNotePhoto(List<IFormFile>? photos, Guid observationNoteId, bool hasAlreadyMainPhotoIndex, int mainPhotoIndex);
        ObservationNote? GetObservationNoteById(Guid observationNoteId);
        void deleteObservationNoteEmbryoStatus(Guid observationNoteId);
        void deleteObservationNoteOperation(Guid observationNoteId);
        void deleteObservationNoteOvumAbnormality(Guid observationNoteId);
        IQueryable<ObservationNotePhoto> GetObservatioNotePhotosByObservationNoteId(Guid observationNoteId);
        void DeleteObservationNotePhoto(Guid observationNoteId);
        void UpdateObservationNotePhoto(IQueryable<ObservationNotePhoto> existingPhotos, List<ObservationNotePhotoDto> inputExistingPhotos);
        void DeleteObservationNotePhoto(IQueryable<ObservationNotePhoto> existingPhotos);
        GetObservationNoteDto? GetExistingObservationNote(Guid observationNoteId);
        List<GetObservationNoteNameDto> GetObservationNoteNameByObservationNoteIds(List<Guid> observationNoteIds);
        List<Common2Dto> GetObservationNoteOvumAbnormalityNameByObservationNoteIds(List<Guid> observationNoteIds);
        List<Common2Dto> GetObservationNoteEmbryoStatuseNameByObservationNoteIds(List<Guid> observationNoteIds);
        List<ObservationNoteOperationDto> GetObservationNoteOperationNameByObservationNoteId(Guid observationNoteId);
        List<GetObservationNoteNameDto> GetFreezeObservationNotes(List<Guid> ovumDetailIds);
        List<GetObservationNoteNameDto> GetFreezeObservationNoteInfosByOvumDetailIds(List<Guid> ovumDetailIds);
    }
}
