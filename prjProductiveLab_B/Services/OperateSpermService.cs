using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using prjProductiveLab_B.Models;

namespace prjProductiveLab_B.Services
{
    public class OperateSpermService : IOperateSpermService
    {
        private readonly ReproductiveLabContext dbContext;
        public OperateSpermService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task GetOriginInfoOfSperm(Guid courseOfTreatmentId)
        {
            var q = dbContext.CourseOfTreatments.Where(x=>x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new OriginInfoOfSpermDto
            {
                customerSqlId = x.Customer.SpouseNavigation.SqlId,
                customerName = x.Customer.SpouseNavigation.Name,
                birthday = x.Customer.SpouseNavigation.Birthday,
                spermRetrievalMethod = dbContext.SpermPickups.Where(y=>y.CourseOfTreatmentId == courseOfTreatmentId).Select(x=>x.SpermRetrievalMethod.Name).FirstOrDefault(),

            });
        }
    }
}
