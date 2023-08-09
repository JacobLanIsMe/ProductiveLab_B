using Reproductive_SharedFunction.Interfaces;
using Reproductive_SharedFunction.Services;
using ReproductiveLab_Common.Dtos.ForFreezeSummary;
using ReproductiveLab_Common.Enums;
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
        private readonly IOvumDetailFunction _ovumDetailFunction;
        //private readonly IPhotoFunction _photoFunction;
        private readonly IErrorFunction _errorFunction;
        private readonly IOvumDetailRepository _ovumDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IObservationNoteRepository _observationNoteRepository;
        public FreezeSummaryService(IOvumDetailFunction ovumDetailFunction,/* IPhotoFunction photo,*/ IErrorFunction errorFunction, IOvumDetailRepository ovumDetailRepository, ICustomerRepository customerRepository, IObservationNoteRepository observationNoteRepository, ISpermFreezeRepository spermFreezeRepository)
        {
            _ovumDetailFunction = ovumDetailFunction;
            _ovumDetailRepository = ovumDetailRepository;
            //_photoFunction = photo;
            _errorFunction = errorFunction;
            _customerRepository = customerRepository;
            _observationNoteRepository = observationNoteRepository;
            _spermFreezeRepository = spermFreezeRepository;
        }
        public List<GetOvumFreezeSummaryDto> GetOvumFreezeSummary(Guid courseOfTreatmentId)
        {
            Guid customerId = _customerRepository.GetCustomerIdByCourseOfTreatmentId(courseOfTreatmentId);
            var customerOvumDetails = _ovumDetailRepository.GetOvumDetailByCustomerId(customerId);
            var customerOvumFreezes = customerOvumDetails.Where(x => x.OvumFreezeId != null && x.OvumThawFreezePairFreezeOvumDetails.Count == 0 && x.OvumTransferPairDonorOvumDetails.Count == 0);
            List<GetOvumFreezeSummaryDto> result = _ovumDetailFunction.GetOvumDetailInfos(customerOvumFreezes);
            //_photoFunction.ConvertPhotoToBase64String(result);
            return result;
        }
        public List<GetOvumFreezeSummaryDto> GetRecipientOvumFreezes(Guid courseOfTreatmentId)
        {
            Guid customerId = _customerRepository.GetCustomerIdByCourseOfTreatmentId(courseOfTreatmentId);
            var customerOvumDetails = _ovumDetailRepository.GetOvumDetailByCustomerId(customerId);
            var recipientOvumFreezes = customerOvumDetails.Where(x => x.FertilizationId == null && x.OvumFreezeId != null);
            List<GetOvumFreezeSummaryDto> result = _ovumDetailFunction.GetOvumDetailInfos(recipientOvumFreezes);
            //_photoFunction.ConvertPhotoToBase64String(result);
            return result;
        }
        public List<GetOvumFreezeSummaryDto> GetDonorOvums(int customerSqlId)
        {
            var customer = _customerRepository.GetCustomerBySqlId(customerSqlId);
            if (customer == null)
            {
                return new List<GetOvumFreezeSummaryDto>();
            }
            Guid customerId = customer.CustomerId;
            var customerOvumDetail = _ovumDetailRepository.GetOvumDetailByCustomerId(customerId);
            var donorOvumFreezes = customerOvumDetail.Where(x => x.FertilizationId == null && x.CourseOfTreatment.OvumSourceId == (int)GermCellSourceEnum.OD && x.OvumTransferPairDonorOvumDetails.Count <= 0);
            List<GetOvumFreezeSummaryDto> result = _ovumDetailFunction.GetOvumDetailInfos(donorOvumFreezes);
            //_photoFunction.ConvertPhotoToBase64String(result);
            return result;
        }
        public List<GetOvumFreezeSummaryDto> GetEmbryoFreezes(Guid courseOfTreatmentId)
        {
            Guid customerId = _customerRepository.GetCustomerIdByCourseOfTreatmentId(courseOfTreatmentId);
            var customerOvumDetails = _ovumDetailRepository.GetOvumDetailByCustomerId(customerId);
            var embryoFreezes = customerOvumDetails.Where(x => x.OvumFreezeId != null && x.FertilizationId != null);
            List<GetOvumFreezeSummaryDto> result = _ovumDetailFunction.GetOvumDetailInfos(embryoFreezes);
            //_photoFunction.ConvertPhotoToBase64String(result);
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
            var customerId = _customerRepository.GetMaleCustomerIdByCourseOfTreatmentId(courseOfTreatmentId);
            if (!customerId.HasValue)
            {
                return new List<GetSpermFreezeSummaryDto>();
            }
            var result = _spermFreezeRepository.GetSpermFreezeSummary((Guid)customerId); ;
            return result;
        }
    }
}
