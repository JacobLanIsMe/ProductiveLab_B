using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Repositories
{
    public class SpermFreezeRepository : ISpermFreezeRepository
    {
        private readonly ReproductiveLabContext _dbContext;
        public SpermFreezeRepository(ReproductiveLabContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<GetSpermFreezeSummaryDto> GetSpermFreezeSummary(Guid customerId)
        {
            return _dbContext.SpermFreezes.Where(x => x.CourseOfTreatment.CustomerId == customerId && !x.SpermThawFreezePairs.Any()).Select(x => new GetSpermFreezeSummaryDto
            {
                spermSource = x.CourseOfTreatment.SpermSource.Name,
                courseOfTreatmentSqlId = x.CourseOfTreatment.SqlId,
                spermSituation = x.CourseOfTreatment.SpermSituation.Name,
                surgicalTime = x.CourseOfTreatment.SurgicalTime,
                freezeTime = x.SpermFreezeSituation.FreezeTime,
                vialNumber = x.VialNumber,
                tankName = x.StorageUnit.StorageStripBox.StorageCanist.StorageTank.TankName,
                canistName = x.StorageUnit.StorageStripBox.StorageCanist.CanistName,
                boxId = x.StorageUnit.StorageStripBoxId,
                unitName = x.StorageUnit.UnitName,
                freezeMediumName = x.SpermFreezeSituation.FreezeMediumInUse.MediumTypeId == (int)MediumTypeEnum.other ? x.SpermFreezeSituation.OtherFreezeMediumName : x.SpermFreezeSituation.FreezeMediumInUse.Name,
            }).OrderBy(x => x.courseOfTreatmentSqlId).ThenBy(x => x.vialNumber).ToList();
        }
    }
}
