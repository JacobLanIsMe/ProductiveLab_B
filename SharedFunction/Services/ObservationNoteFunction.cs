using Microsoft.AspNetCore.Http;
using Reproductive_SharedFunction.Interfaces;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Services
{
    public class ObservationNoteFunction : IObservationNoteFunction
    {
        private readonly IObservationNoteRepository _observationNoteRepository;
        public ObservationNoteFunction(IObservationNoteRepository observationNoteRepository)
        {
            _observationNoteRepository = observationNoteRepository;
        }
        
        public void AddObservationNoteEmbryoStatus(Guid observationNoteId, AddObservationNoteDto input)
        {
            List<int> embryoStatusIds = JsonSerializer.Deserialize<List<int>>(input.embryoStatusId);
            _observationNoteRepository.AddObservationNoteEmbryoStatus(observationNoteId, embryoStatusIds);
        }
        public void AddObservationNoteOvumAbnormality(Guid observationNoteId, AddObservationNoteDto input)
        {
            List<int> ovumAbnormalityIds = JsonSerializer.Deserialize<List<int>>(input.ovumAbnormalityId);
            _observationNoteRepository.AddObservationNoteOvumAbnormality(observationNoteId, ovumAbnormalityIds);
        }
        public void AddObservationNoteOperation(Guid observationNoteId, AddObservationNoteDto input)
        {
            List<int> operationTypeIds = JsonSerializer.Deserialize<List<int>>(input.operationTypeId);
            _observationNoteRepository.AddObservationNoteOperation(observationNoteId, input, operationTypeIds);
        }
        public async Task AddObservationNotePhoto(List<IFormFile>? photos, string inputMainPhotoIndex, Guid observationNoteId, bool hasAlreadyMainPhotoIndex)
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
                await _observationNoteRepository.AddObservationNotePhoto(photos, observationNoteId, hasAlreadyMainPhotoIndex, mainPhotoIndex);
            }
        }
        public void DeleteObservationNoteEmbryoStatus(Guid observationNoteId)
        {
            _observationNoteRepository.deleteObservationNoteEmbryoStatus(observationNoteId);
        }
        public void DeleteObservationNoteOperation(Guid observationNoteId)
        {
            _observationNoteRepository.deleteObservationNoteOperation(observationNoteId);
        }
        public void DeleteObservationNoteOvumAbnormality(Guid observationNoteId)
        {
            _observationNoteRepository.deleteObservationNoteOvumAbnormality(observationNoteId);
        }
    }
}
