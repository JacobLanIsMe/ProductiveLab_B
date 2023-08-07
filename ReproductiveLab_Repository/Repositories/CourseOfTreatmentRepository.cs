using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForTreatment;
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
    public class CourseOfTreatmentRepository : ICourseOfTreatmentRepository
    {
        private readonly ReproductiveLabContext _db;
        public CourseOfTreatmentRepository(ReproductiveLabContext db)
        {
            _db = db;
        }
        public CourseOfTreatment? GetCourseOfTreatmentById(Guid courseOfTreatmentId)
        {
            return _db.CourseOfTreatments.FirstOrDefault(x => x.CourseOfTreatmentId == courseOfTreatmentId);
        }
        public List<LabMainPageDto> GetMainPageInfo()
        {
            List<LabMainPageDto> result = _db.CourseOfTreatments.Where(x => !x.IsTerminated).Select(x =>
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
        public void AddCourseOfTreatment(AddCourseOfTreatmentDto input)
        {
            CourseOfTreatment course = new CourseOfTreatment
            {
                Doctor = input.doctorId,
                CustomerId = input.customerId,
                SurgicalTime = input.surgicalTime,
                TreatmentStatusId = (int)TreatmentStatusEnum.inTreatment,
                Memo = input.memo,
            };
            if (int.TryParse(input.ovumSituationId, out int ovumSituationId))
            {
                course.OvumSituationId = ovumSituationId;
            }
            if (int.TryParse(input.ovumSourceId, out int ovumSourceId))
            {
                course.OvumSourceId = ovumSourceId;
            }
            if (int.TryParse(input.ovumOperationId, out int ovumOperationId))
            {
                course.OvumOperationId = ovumOperationId;
            }
            if (int.TryParse(input.spermSituationId, out int spermSituationId))
            {
                course.SpermSituationId = spermSituationId;
            }
            if (int.TryParse(input.spermSourceId, out int spermSourceId))
            {
                course.SpermSourceId = spermSourceId;
            }
            if (int.TryParse(input.spermOperationId, out int spermOperationId))
            {
                course.SpermOperationId = spermOperationId;
            }
            if (int.TryParse(input.SpermRetrievalMethodId, out int spermRetrievalMethodId))
            {
                course.SpermRetrievalMethodId = spermRetrievalMethodId;
            }
            if (int.TryParse(input.embryoSituationId, out int embryoSituationId))
            {
                course.EmbryoSituationId = embryoSituationId;
            }
            if (int.TryParse(input.embryoOperationId, out int embryoOperationId))
            {
                course.EmbryoOperationId = embryoOperationId;
            }
            _db.CourseOfTreatments.Add(course);
            _db.SaveChanges();
        }
        public Guid GetCourseOfTreatmentIdBySqlId(int sqlId)
        {
            return _db.CourseOfTreatments.Where(x => x.SqlId == sqlId).Select(x => x.CourseOfTreatmentId).FirstOrDefault();
        }
    }
}
