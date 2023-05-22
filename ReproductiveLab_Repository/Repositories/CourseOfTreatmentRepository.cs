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
    }
}
