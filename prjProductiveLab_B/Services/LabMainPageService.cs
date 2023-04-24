using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;

namespace prjProductiveLab_B.Services
{
    public class LabMainPageService : ILabMainPage
    {
        private readonly ReproductiveLabContext dbContext;
        public LabMainPageService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<LabMainPageDto>> GetMainPageInfo()
        {
            List<LabMainPageDto> result = await dbContext.CourseOfTreatments.Select(x =>
            new LabMainPageDto
            {
                surgicalTime = x.SurgicalTime,
                courseOfTreatmentSqlId = x.SqlId,
                courseOfTreatmentId = x.CourseOfTreatmentId.ToString(),
                medicalRecordNumber = x.Customer.SqlId,
                name = x.Customer.Name,
                doctor = x.DoctorNavigation.Name,
                treatment = new TreatmentDto
                {
                    treatmentId = x.TreatmentId,
                    ovumSituationName = x.Treatment.OvumSituation.Name,
                    ovumSourceName = x.Treatment.OvumSource.Name,
                    ovumOperationName = x.Treatment.OvumOperation.Name,
                    spermSituationName = x.Treatment.SpermSituation.Name,
                    spermSourceName = x.Treatment.SpermSource.Name,
                    spermOperationName = x.Treatment.SpermOperation.Name,
                    spermRetrievalMethodName = x.Treatment.SpermRetrievalMethod.Name,
                    embryoSituationName = x.Treatment.EmbryoSituation.Name,
                    embryoOperationName = x.Treatment.EmbryoOperation.Name
                },
                treatmentStatus = x.TreatmentStatus.Name,
                ovumFromCourseOfTreatmentId = x.OvumFromCourseOfTreatmentId,
                spermFromCourseOfTreatmentId = x.SpermFromCourseOfTreatmentId
            }).OrderBy(x=>x.surgicalTime).ToListAsync();
            foreach (var i in result)
            {
                TimeSpan treatmentDay = DateTime.Now.Date - i.surgicalTime.Date;
                i.treatmentDay = $"D{treatmentDay.Days}";
            }
            return result;
        }
    }
}
