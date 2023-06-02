using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Interfaces
{
    public interface IPhotoFunction
    {
        void ConvertPhotoToBase64String(List<GetOvumFreezeSummaryDto> result);
        string? GetBase64String(string? photoName);
        void GetObservationNotePhotoBase64String(List<ObservationNotePhotoDto> observationNotePhotos);
    }
}
