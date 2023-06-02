using Microsoft.AspNetCore.Http;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Interfaces
{
    public interface IObservationNoteFunction
    {
        void AddObservationNoteEmbryoStatus(Guid observationNoteId, AddObservationNoteDto input);
        void AddObservationNoteOvumAbnormality(Guid observationNoteId, AddObservationNoteDto input);
        void AddObservationNoteOperation(Guid observationNoteId, AddObservationNoteDto input);
        void AddObservationNotePhoto(List<IFormFile>? photos, string inputMainPhotoIndex, Guid observationNoteId, bool hasAlreadyMainPhotoIndex);
        void DeleteObservationNoteEmbryoStatus(Guid observationNoteId);
        void DeleteObservationNoteOperation(Guid observationNoteId);
        void DeleteObservationNoteOvumAbnormality(Guid observationNoteId);
    }
}
