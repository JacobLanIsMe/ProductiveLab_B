using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos.ForTreatment;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Repositories
{
    public class OvumFreezeRepository : IOvumFreezeRepository
    {
        private readonly ReproductiveLabContext _db;
        public OvumFreezeRepository(ReproductiveLabContext db)
        {
            _db = db;
        }

        public void AddOvumFreeze(AddOvumFreezeDto input)
        {
            OvumFreeze ovumFreeze = new OvumFreeze
            {
                FreezeTime = input.freezeTime,
                Embryologist = input.embryologist,
                StorageUnitId = input.storageUnitId,
                MediumInUseId = input.mediumInUseId,
                OtherMediumName = input.otherMediumName,
                Memo = input.memo,
                OvumMorphologyA = input.ovumMorphology_A,
                OvumMorphologyB = input.ovumMorphology_B,
                OvumMorphologyC = input.ovumMorphology_C,
                TopColorId = input.topColorId,
                IsThawed = false
            };
            _db.OvumFreezes.Add(ovumFreeze);
            _db.SaveChanges();
        }
        public Guid GetLatestOvumFreezedId()
        {
            return _db.OvumFreezes.OrderByDescending(x => x.SqlId).Select(x => x.OvumFreezeId).FirstOrDefault();
        }
        public OvumFreeze? GetOvumFreezeByOvumDetailId(Guid ovumDetailId)
        {
            return _db.OvumDetails.Where(x => x.OvumDetailId == ovumDetailId).Select(x => x.OvumFreeze).FirstOrDefault();
        }
        public void UpdateOvumFreeze(OvumFreeze ovumFreeze, AddOvumFreezeDto input)
        {
            ovumFreeze.FreezeTime = input.freezeTime;
            ovumFreeze.Embryologist = input.embryologist;
            ovumFreeze.OvumMorphologyA = input.ovumMorphology_A;
            ovumFreeze.OvumMorphologyB = input.ovumMorphology_B;
            ovumFreeze.OvumMorphologyC = input.ovumMorphology_C;
            ovumFreeze.MediumInUseId = input.mediumInUseId;
            ovumFreeze.OtherMediumName = input.otherMediumName;
            ovumFreeze.Memo = input.memo;
            _db.SaveChanges();
        }
        public AddOvumFreezeDto? GetOvumFreezeDtoByOvumDetailId(Guid ovumDetailId)
        {
            return _db.OvumDetails.Where(x => x.OvumDetailId == ovumDetailId).Select(x => new AddOvumFreezeDto
            {
                freezeTime = x.OvumFreeze == null ? default : x.OvumFreeze.FreezeTime,
                embryologist = x.OvumFreeze == null ? default : x.OvumFreeze.Embryologist,
                mediumInUseId = x.OvumFreeze == null ? default : x.OvumFreeze.MediumInUseId,
                otherMediumName = x.OvumFreeze == null ? default : x.OvumFreeze.OtherMediumName,
                ovumMorphology_A = x.OvumFreeze == null ? default : x.OvumFreeze.OvumMorphologyA,
                ovumMorphology_B = x.OvumFreeze == null ? default : x.OvumFreeze.OvumMorphologyB,
                ovumMorphology_C = x.OvumFreeze == null ? default : x.OvumFreeze.OvumMorphologyC,
                memo = x.OvumFreeze == null ? default : x.OvumFreeze.Memo
            }).FirstOrDefault();
        }
    }
}
