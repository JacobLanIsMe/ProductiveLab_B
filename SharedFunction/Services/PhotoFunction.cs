using Microsoft.AspNetCore.Hosting;
using Reproductive_SharedFunction.Interfaces;
using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Services
{
    public class PhotoFunction : IPhotoFunction
    {
        private readonly IWebHostEnvironment _env;
        public PhotoFunction(IWebHostEnvironment env)
        {
            _env = env;
        }
        //public void ConvertPhotoToBase64String(List<GetOvumFreezeSummaryDto> result)
        //{
        //    foreach (var i in result)
        //    {
        //        if (i.freezeObservationNoteInfo != null && i.freezeObservationNoteInfo.observationNotePhotos != null && i.freezeObservationNoteInfo.observationNotePhotos.Count > 0 && !string.IsNullOrEmpty(i.freezeObservationNoteInfo.observationNotePhotos[0].photoName))
        //        {
        //            i.freezeObservationNoteInfo.observationNotePhotos[0].imageBase64String = GetBase64String(i.freezeObservationNoteInfo.observationNotePhotos[0].photoName);
        //        }
        //    }
        //}
        //public string? GetBase64String(string? photoName)
        //{
        //    if (photoName == null)
        //    {
        //        return null;
        //    }
        //    string path = Path.Combine(_env.ContentRootPath, "uploads", "images", photoName);
        //    if (File.Exists(path))
        //    {
        //        return Convert.ToBase64String(File.ReadAllBytes(path));
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        //public void GetObservationNotePhotoBase64String(List<ObservationNotePhotoDto> observationNotePhotos)
        //{
        //    foreach (var i in observationNotePhotos)
        //    {
        //        i.imageBase64String = GetBase64String(i.photoName);
        //    }
        //}
    }
}
