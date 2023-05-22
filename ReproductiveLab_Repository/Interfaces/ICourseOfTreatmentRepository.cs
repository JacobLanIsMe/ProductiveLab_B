using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Interfaces
{
    public interface ICourseOfTreatmentRepository
    {
        CourseOfTreatment? GetCourseOfTreatmentById(Guid courseOfTreatmentId);
    }
}
