using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly ICourseOfTreatmentRepository _courseOfTreatmentRepository;
        public TreatmentService(ICourseOfTreatmentRepository courseOfTreatmentRepository)
        {
            _courseOfTreatmentRepository = courseOfTreatmentRepository;
        }
        public List<LabMainPageDto> GetMainPageInfo()
        {
            List<LabMainPageDto> result = _courseOfTreatmentRepository.GetMainPageInfo();
            foreach (var i in result)
            {
                TimeSpan treatmentDay = DateTime.Now.Date - i.surgicalTime.Date;
                i.treatmentDay = $"D{treatmentDay.Days}";
            }
            return result;
        }
    }
}
