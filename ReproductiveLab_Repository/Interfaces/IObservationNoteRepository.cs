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
        List<ObservationNoteDto> GetObservationNote(Guid courseOfTreatmentId);
        List<Common1Dto> GetOvumMaturation();
        List<Common1Dto> GetObservationType();
        List<Common1Dto> GetOvumAbnormality();
        List<Common1Dto> GetFertilizationResult();
        Task<List<Common1Dto>> GetBlastomereScoreC();
        Task<List<Common1Dto>> GetBlastomereScoreG();
        Task<List<Common1Dto>> GetBlastomereScoreF();
        List<Common1Dto> GetEmbryoStatus();
        Task<List<Common1Dto>> GetBlastocystScoreExpansion();
        Task<List<Common1Dto>> GetBlastocystScoreIce();
        Task<List<Common1Dto>> GetBlastocystScoreTe();
        List<Common1Dto> GetOperationType();
        void AddObservationNote(ObservationNote input);
        Guid GetLatestObservationNoteId();
        void AddObservationNoteEmbryoStatus(Guid observationNoteId, List<int> embryoStatusIds);
        void AddObservationNoteOvumAbnormality(Guid observationNoteId, List<int> ovumAbnormalityIds);
        void AddObservationNoteOperation(Guid observationNoteId, AddObservationNoteDto input, List<int> operationTypeIds);
        void AddObservationNotePhoto(List<IFormFile>? photos, Guid observationNoteId, bool hasAlreadyMainPhotoIndex, int mainPhotoIndex);
        ObservationNote? GetObservationNoteById(Guid observationNoteId);
        void deleteObservationNoteEmbryoStatus(Guid observationNoteId);
        void deleteObservationNoteOperation(Guid observationNoteId);
        void deleteObservationNoteOvumAbnormality(Guid observationNoteId);
        IQueryable<ObservationNotePhoto> GetObservatioNotePhotosByObservationNoteId(Guid observationNoteId);
        void UpdateObservationNotePhoto(IQueryable<ObservationNotePhoto> existingPhotos, List<ObservationNotePhotoDto> inputExistingPhotos);
        void DeleteObservationNotePhoto(IQueryable<ObservationNotePhoto> existingPhotos);
    }
}
