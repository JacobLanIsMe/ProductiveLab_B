using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForOperateSperm;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Common.Models;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Repositories
{
    public class OperateSpermRepository : IOperateSpermRepository
    {
        private readonly ReproductiveLabContext _db;
        private readonly IMediumRepository _mediumRepository;
        public OperateSpermRepository(ReproductiveLabContext db, IMediumRepository mediumRepository)
        {
            _db = db;
            _mediumRepository = mediumRepository;
        }

        public List<SpermFreezeDto> GetSpermFreeze(int customerSqlId)
        {
            return _db.SpermFreezes.Where(x => x.CourseOfTreatment.Customer.SqlId == customerSqlId && x.IsThawed == false).Select(x => new SpermFreezeDto
            {
                spermFreezeId = x.SpermFreezeId,
                vialNumber = x.VialNumber,
                storageUnitName = x.StorageUnit.UnitName,
                storageStripBoxId = x.StorageUnit.StorageStripBoxId,
                storageCanistName = x.StorageUnit.StorageStripBox.StorageCanist.CanistName,
                storageTankName = x.StorageUnit.StorageStripBox.StorageCanist.StorageTank.TankName,
                storageUnitId = x.StorageUnitId,
                freezeTime = x.SpermFreezeSituation.FreezeTime
            }).OrderBy(x => x.freezeTime).ThenBy(x => x.vialNumber).ToList();
        }
        public List<SpermScoreModel> GetSpermScoresByCourseOfTreatmentId(Guid courseOfTreatmentId)
        {
            return _db.SpermScores.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new SpermScoreModel
            {
                isThawed = x.CourseOfTreatment.SpermThaws.Any() ? true : false,
                baseSpermInfo_Thaw = new BaseOperateSpermInfoDto
                {
                    spermSituationName = x.CourseOfTreatment.SpermSituation == null ? null : x.CourseOfTreatment.SpermSituation.Name,
                    //spermRetrievalMethodName = x.CourseOfTreatment.SpermThaws.Any() ? x.CourseOfTreatment
                    spermOwner = new BaseCustomerInfoDto
                    {
                        customerName = x.CourseOfTreatment.SpermThaws.Any() ? x.CourseOfTreatment.SpermThaws.First().SpermThawFreezePairs.Select(y => y.SpermFreeze.CourseOfTreatment.Customer.Name).FirstOrDefault() : null,
                        customerSqlId = x.CourseOfTreatment.SpermThaws.Any() ? x.CourseOfTreatment.SpermThaws.First().SpermThawFreezePairs.Select(y => y.SpermFreeze.CourseOfTreatment.Customer.SqlId).FirstOrDefault() : default,
                    }
                },
                baseSpermInfo_Fresh = new BaseOperateSpermInfoDto
                {
                    spermSituationName = x.CourseOfTreatment.SpermSituation == null ? null : x.CourseOfTreatment.SpermSituation.Name,
                    spermRetrievalMethodName = x.CourseOfTreatment.SpermRetrievalMethod.Name,
                    spermOwner = new BaseCustomerInfoDto
                    {
                        customerName = x.CourseOfTreatment.Customer.GenderId == (int)GenderEnum.female && x.CourseOfTreatment.Customer.SpouseNavigation != null ? x.CourseOfTreatment.Customer.SpouseNavigation.Name : x.CourseOfTreatment.Customer.Name,
                        customerSqlId = x.CourseOfTreatment.Customer.GenderId == (int)GenderEnum.female && x.CourseOfTreatment.Customer.SpouseNavigation != null ? x.CourseOfTreatment.Customer.SpouseNavigation.SqlId : x.CourseOfTreatment.Customer.SqlId
                    }
                },
                volume = x.Volume,
                concentration = x.Concentration,
                activityA = x.ActivityA,
                activityB = x.ActivityB,
                activityC = x.ActivityC,
                activityD = x.ActivityD,
                morphology = x.Morphology,
                abstinence = x.Abstinence,
                spermScoreTimePointId = x.SpermScoreTimePointId,
                spermScoreTimePoint = x.SpermScoreTimePoint.TimePoint,
                recordTime = x.RecordTime,
                embryologist = x.Embryologist,
                embryologistName = x.EmbryologistNavigation.Name,
                courseOfTreatmentId = x.CourseOfTreatmentId,
                courseOfTreatmentSqlId = x.CourseOfTreatment.SqlId
            }).OrderBy(x => x.spermScoreTimePointId).ThenBy(x => x.recordTime).ToList();
        }

        public void AddSpermScore(SpermScoreDto addSpermScore)
        {
            SpermScore spermScore = new SpermScore();
            spermScore = TransformSpermScoreDtoToSpermScore(addSpermScore, spermScore);
            _db.SpermScores.Add(spermScore);
            _db.SaveChanges();
        }
        public SpermScore? GetExistingSpermScoreByCourseOfTreatmentId(Guid courseOfTreatmentId, int spermScoreTimePointId)
        {
            return _db.SpermScores.FirstOrDefault(x => x.CourseOfTreatmentId == courseOfTreatmentId && x.SpermScoreTimePointId == spermScoreTimePointId);
        }
        public void UpdateSpermScore(SpermScoreDto addSpermScore, SpermScore existingSpermScore)
        {
            TransformSpermScoreDtoToSpermScore(addSpermScore, existingSpermScore);
            _db.SaveChanges();
        }

        public List<Common1Dto> GetSpermFreezeOperationMethod()
        {
            return _db.SpermFreezeOperationMethods.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).AsNoTracking().ToList();
        }
        public void AddSpermFreezeSituation(AddSpermFreezeDto input)
        {
            SpermFreezeSituation situation = new SpermFreezeSituation
            {
                Embryologist = input.embryologist,
                FreezeTime = input.freezeTime,
                SpermFreezeOperationMethodId = input.spermFreezeOperationMethodId,
                FreezeMediumInUseId = input.freezeMedium,
                OtherFreezeMediumName = input.otherFreezeMediumName
            };
            _mediumRepository.SetMediumInUse<SpermFreezeSituation>(situation, input.mediumInUseArray);
            _db.SpermFreezeSituations.Add(situation);
            _db.SaveChanges();
        }
        public Guid GetLatestSpermFreezeSituationId()
        {
            return _db.SpermFreezeSituations.OrderByDescending(x => x.SqlId).Select(x => x.SpermFreezeSituationId).FirstOrDefault();
        }
        public void AddSpermFreeze(AddSpermFreezeDto input, Guid lastSituationId)
        {
            for (int i = 0; i < input.storageUnitId.Count; i++)
            {
                SpermFreeze spermFreeze = new SpermFreeze()
                {
                    VialNumber = i + 1,
                    CourseOfTreatmentId = input.courseOfTreatmentId,
                    StorageUnitId = input.storageUnitId[i],
                    IsThawed = false,
                    SpermFreezeSituationId = lastSituationId
                };
                _db.SpermFreezes.Add(spermFreeze);
                var storageUnit = _db.StorageUnits.FirstOrDefault(x => x.SqlId == input.storageUnitId[i]);
                if (storageUnit != null && storageUnit.IsOccupied == false)
                {
                    storageUnit.IsOccupied = true;
                }
                else
                {
                    throw new Exception("儲位資訊有誤");
                }
            }
            _db.SaveChanges();
        }
        public List<Common1Dto> GetSpermThawMethods()
        {
            return _db.SpermThawMethods.Select(x => new Common1Dto
            {
                id = x.SqlId,
                name = x.Name
            }).OrderBy(x => x.id).ToList();
        }
        public void AddSpermThaw(AddSpermThawDto input)
        {
            SpermThaw spermThaw = new SpermThaw
            {
                CourseOfTreatmentId = input.courseOfTreatmentId,
                SpermThawMethodId = input.spermThawMethodId,
                ThawTime = input.thawTime,
                Embryologist = input.embryologist,
                RecheckEmbryologist = input.recheckEmbryologist,
                OtherSpermThawMethod = input.otherSpermThawMethod,
            };
            _mediumRepository.SetMediumInUse<SpermThaw>(spermThaw, input.mediumInUseIds);
            _db.SpermThaws.Add(spermThaw);
            _db.SaveChanges();
        }
        public Guid GetLatestSpermThawId()
        {
            return _db.SpermThaws.OrderByDescending(x => x.SqlId).Select(x => x.SpermThawId).FirstOrDefault();
        }
        public void AddSpermThawFreezePair(List<Guid> spermFreezeIds, Guid latestSpermThawId)
        {
            var spermFreezes = _db.SpermFreezes.Where(x => spermFreezeIds.Contains(x.SpermFreezeId)).Select(x => new
            {
                spermFreeze = x,
                storageUnit = x.StorageUnit
            });
            foreach (var i in spermFreezes)
            {
                SpermThawFreezePair pair = new SpermThawFreezePair
                {
                    SpermThawId = latestSpermThawId,
                    SpermFreezeId = i.spermFreeze.SpermFreezeId
                };
                _db.SpermThawFreezePairs.Add(pair);
                i.spermFreeze.IsThawed = true;
                i.storageUnit.IsOccupied = false;
            }
            _db.SaveChanges();
        }




        private SpermScore TransformSpermScoreDtoToSpermScore(SpermScoreDto spermScoreDto, SpermScore spermScore)
        {

            spermScore.Volume = spermScoreDto.volume;
            spermScore.Concentration = spermScoreDto.concentration;
            spermScore.ActivityA = spermScoreDto.activityA;
            spermScore.ActivityB = spermScoreDto.activityB;
            spermScore.ActivityC = spermScoreDto.activityC;
            spermScore.ActivityD = spermScoreDto.activityD;
            spermScore.Morphology = spermScoreDto.morphology;
            spermScore.Abstinence = spermScoreDto.abstinence;
            spermScore.SpermScoreTimePointId = spermScoreDto.spermScoreTimePointId;
            spermScore.RecordTime = spermScoreDto.recordTime;
            spermScore.Embryologist = spermScoreDto.embryologist;
            spermScore.CourseOfTreatmentId = spermScoreDto.courseOfTreatmentId;
            return spermScore;
        }
        
    }
}
