using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Common.Interfaces;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLab_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Service.Services
{
    public class FreezeSummaryService : IFreezeSummaryService
    {
        private readonly ISpermFreezeRepository _spermFreezeRepository;
        private readonly ISharedFunction _sharedFunction;
        private readonly IOvumDetailRepository _ovumDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IObservationNoteRepository _observationNoteRepository;
        public FreezeSummaryService(ISharedFunction sharedFunction, IOvumDetailRepository ovumDetailRepository, ICustomerRepository customerRepository, IObservationNoteRepository observationNoteRepository, ISpermFreezeRepository spermFreezeRepository)
        {
            _sharedFunction = sharedFunction;
            _ovumDetailRepository = ovumDetailRepository;
            _customerRepository = customerRepository;
            _observationNoteRepository = observationNoteRepository;
            _spermFreezeRepository = spermFreezeRepository;
        }
        public List<GetOvumFreezeSummaryDto> GetOvumFreezeSummary(Guid courseOfTreatmentId)
        {
            Guid? customerId = _customerRepository.GetCustomerIdByCourseOfTreatmentId(courseOfTreatmentId);
            if (!customerId.HasValue)
            {
                return new List<GetOvumFreezeSummaryDto>();
            }
            var customerOvumDetails = _ovumDetailRepository.GetCustomerOvumDetail((Guid)customerId);
            var customerOvumFreezes = customerOvumDetails.Where(x => x.OvumFreezeId != null && x.OvumThawFreezePairFreezeOvumDetails.Count == 0 && x.OvumTransferPairDonorOvumDetails.Count == 0);
            List<GetOvumFreezeSummaryDto> result = _sharedFunction.GetOvumDetailInfos(customerOvumFreezes);
            _sharedFunction.ConvertPhotoToBase64String(result);
            return result;
        }
        public List<GetOvumFreezeSummaryDto> GetRecipientOvumFreezes(Guid courseOfTreatmentId)
        {
            Guid? customerId = _customerRepository.GetCustomerIdByCourseOfTreatmentId(courseOfTreatmentId);
            if (!customerId.HasValue)
            {
                return new List<GetOvumFreezeSummaryDto>();
            }
            var customerOvumDetail = _ovumDetailRepository.GetCustomerOvumDetail((Guid)customerId);
            var recipientOvumFreezes = customerOvumDetail.Where(x => x.FertilizationId == null && x.OvumFreezeId != null);
            List<GetOvumFreezeSummaryDto> result = _sharedFunction.GetOvumDetailInfos(recipientOvumFreezes);
            _sharedFunction.ConvertPhotoToBase64String(result);
            return result;
        }
        public List<GetOvumFreezeSummaryDto> GetDonorOvums(int customerSqlId)
        {
            var customer = _customerRepository.GetCustomerBySqlId(customerSqlId);
            _sharedFunction.ThrowExceptionIfNull(customer, "CustomerSqlId is not found");
            Guid customerId = customer.CustomerId;
            var customerOvumDetail = _ovumDetailRepository.GetCustomerOvumDetail(customerId);
            var donorOvumFreezes = customerOvumDetail.Where(x => x.FertilizationId == null && x.CourseOfTreatment.OvumSourceId == (int)GermCellSourceEnum.OD && x.OvumTransferPairDonorOvumDetails.Count <= 0);
            List<GetOvumFreezeSummaryDto> result = _sharedFunction.GetOvumDetailInfos(donorOvumFreezes);
            _sharedFunction.ConvertPhotoToBase64String(result);
            return result;
        }
        public List<GetOvumFreezeSummaryDto> GetEmbryoFreezes(Guid courseOfTreatmentId)
        {
            Guid? customerId = _customerRepository.GetCustomerIdByCourseOfTreatmentId(courseOfTreatmentId);
            if (!customerId.HasValue)
            {
                return new List<GetOvumFreezeSummaryDto>();
            }
            var customerOvumDetail = _ovumDetailRepository.GetCustomerOvumDetail((Guid)customerId);
            var embryoFreezes = customerOvumDetail.Where(x => x.OvumFreezeId != null && x.FertilizationId != null);
            List<GetOvumFreezeSummaryDto> result = _sharedFunction.GetOvumDetailInfos(embryoFreezes);
            _sharedFunction.ConvertPhotoToBase64String(result);
            return result;
        }
        public List<Guid> GetUnFreezingObservationNoteOvumDetails(List<Guid> ovumDetailIds)
        {
            var observationNotes = _observationNoteRepository.GetObservationNotesByOvumDetailIds(ovumDetailIds);
            var freezingObservationNoteOvumDetailIds = observationNotes.Where(x=> x.ObservationTypeId == (int)ObservationTypeEnum.freezeObservation).Select(x=>x.OvumDetailId).ToList();
            var unFreezingObservationNoteOvumDetails = ovumDetailIds.Where(x => !freezingObservationNoteOvumDetailIds.Contains(x)).ToList();
            return unFreezingObservationNoteOvumDetails;
        }
        public List<GetSpermFreezeSummaryDto> GetSpermFreezeSummary(Guid courseOfTreatmentId)
        {
            var customerId = _customerRepository.GetCustomerIdByCourseOfTreatmentId(courseOfTreatmentId);
            if (!customerId.HasValue)
            {
                return new List<GetSpermFreezeSummaryDto>();
            }
            return _spermFreezeRepository.GetSpermFreezeSummary((Guid)customerId);
        }
    }
}
