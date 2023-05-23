using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
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
    public class CourseOfTreatmentRepository : ICourseOfTreatmentRepository
    {
        private readonly ReproductiveLabContext _dbContext;
        public CourseOfTreatmentRepository(ReproductiveLabContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CourseOfTreatment? GetCourseOfTreatmentById(Guid courseOfTreatmentId)
        {
            return _dbContext.CourseOfTreatments.FirstOrDefault(x=>x.CourseOfTreatmentId == courseOfTreatmentId);
        }
        public List<LabMainPageDto> GetMainPageInfo()
        {
            List<LabMainPageDto> result = _dbContext.CourseOfTreatments.Select(x =>
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
                    ovumSituationName = x.OvumSituation.Name,
                    ovumSourceName = x.OvumSource.Name,
                    ovumOperationName = x.OvumOperation.Name,
                    spermSituationName = x.SpermSituation.Name,
                    spermSourceName = x.SpermSource.Name,
                    spermOperationName = x.SpermOperation.Name,
                    spermRetrievalMethodName = x.SpermRetrievalMethod.Name,
                    embryoSituationName = x.EmbryoSituation.Name,
                    embryoOperationName = x.EmbryoOperation.Name
                },
                treatmentStatus = x.TreatmentStatus.Name
            }).OrderByDescending(x => x.surgicalTime).ToList();
            
            return result;
        }
    }
}
