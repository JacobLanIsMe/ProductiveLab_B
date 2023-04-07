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
                treatmentName = x.Treatment.Name,
                treatmentStatus = x.TreatmentStatus.Name
       
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
