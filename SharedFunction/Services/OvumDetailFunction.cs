using Microsoft.EntityFrameworkCore;
using Reproductive_SharedFunction.Interfaces;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using ReproductiveLab_Common.Dtos.ForObservationNote;
using ReproductiveLab_Common.Dtos.ForStorage;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Services
{
    public class OvumDetailFunction : IOvumDetailFunction
    {
        private readonly IOvumDetailRepository _ovumDetailRepository;
        private readonly IObservationNoteRepository _observationNoteRepository;
        private readonly IStorageRepository _storageRepository;

        public OvumDetailFunction(IOvumDetailRepository ovumDetailRepository, IObservationNoteRepository observationNoteRepository, IStorageRepository storageRepository)
        {
            _ovumDetailRepository = ovumDetailRepository;
            _observationNoteRepository = observationNoteRepository;
            _storageRepository = storageRepository;
        }


        public List<GetOvumFreezeSummaryDto> GetOvumDetailInfos(IQueryable<OvumDetail> ovumDetails)
        {
            var baseInfos = _ovumDetailRepository.GetBaseOvumDetailInfosByOvumDetails(ovumDetails);
            List<Guid> ovumDetailIds = ovumDetails.Select(x => x.OvumDetailId).ToList();
            var freezeObservationNotes = _observationNoteRepository.GetFreezeObservationNoteInfosByOvumDetailIds(ovumDetailIds);
            var storageInfos = _storageRepository.GetStorageInfosByOvumDetailIds(ovumDetailIds);
            foreach (var i in baseInfos)
            {
                i.freezeObservationNoteInfo = freezeObservationNotes.Where(x => x.ovumDetailId == i.ovumDetailId).FirstOrDefault();
                i.freezeStorageInfo = storageInfos.Where(x => x.OvumDetailId == i.ovumDetailId).FirstOrDefault();
            }
            List<GetOvumFreezeSummaryDto> result = baseInfos.Where(x => !x.isThawed).ToList();
            return result;
        }
    }
}
