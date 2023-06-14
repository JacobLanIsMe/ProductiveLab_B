using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Enums;
using ReproductiveLab_Common.Models;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ReproductiveLabContext _db;
        public CustomerRepository(ReproductiveLabContext db)
        {
            _db = db;
        }
        public Guid? GetMaleCustomerIdByCourseOfTreatmentId(Guid courseOfTreatmentId)
        {
            var result = _db.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => x.SpermOperationId == (int)GermCellOperationEnum.freeze ? x.CustomerId : x.Customer.Spouse).FirstOrDefault();
            return result;
        }
        public Guid GetCustomerIdByCourseOfTreatmentId(Guid courseOfTreatmentId)
        {
            var result = _db.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => x.CustomerId).FirstOrDefault();
            return result;
        }
        public void AddCustomer(CustomerModel input)
        {
            Customer customer = new Customer
            {
                Name = input.name,
                GenderId = input.genderId,
                Birthday = input.birthday,
                Spouse = input.spouse
            };
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }
        public Customer? GetLatestCustomer()
        {
            return _db.Customers.OrderByDescending(x => x.SqlId).FirstOrDefault();
        }
        public Customer? GetCustomerById(Guid customerId)
        {
            return _db.Customers.FirstOrDefault(x => x.CustomerId == customerId);
        }
        public void UpdateSpouse(Customer customer, Guid spouseCustomerId)
        {
            customer.Spouse = spouseCustomerId;
            _db.SaveChanges();
        }

        public List<Gender> GetGenders()
        {
            return _db.Genders.ToList();
        }
        public Customer? GetCustomerBySqlId(int customerSqlId)
        {
            return _db.Customers.FirstOrDefault(x=>x.SqlId == customerSqlId);
        }
        public BaseCustomerInfoDto GetBaseCustomerInfoBySqlId(int customerSqlId)
        {
            var customer = GetCustomerBySqlId(customerSqlId);
            BaseCustomerInfoDto result = new BaseCustomerInfoDto();
            if (customer == null)
            {
                return result;
            }
            result.customerId = customer.CustomerId;
            result.customerSqlId = customer.SqlId;
            result.customerName = customer.Name;
            return result;
        }
        public Guid GetCustomerIdByOvumDetailId(Guid ovumDetailId)
        {
            return _db.OvumDetails.Where(x => x.OvumDetailId == ovumDetailId).Select(x => x.CourseOfTreatment.CustomerId).FirstOrDefault();
        }
        public List<BaseCustomerInfoDto> GetAllCustomer()
        {
            return _db.Customers.Select(x => new BaseCustomerInfoDto
            {
                customerId = x.CustomerId,
                customerSqlId = x.SqlId,
                customerName = x.Name,
                birthday = x.Birthday
            }).OrderBy(x => x.customerSqlId).ToList();
        }
        
        public BaseCustomerInfoDto GetBaseCustomerInfoByCourseOfTreatmentId(Guid courseOfTreatmentId)
        {
            var result = _db.CourseOfTreatments.Where(x => x.CourseOfTreatmentId == courseOfTreatmentId).Select(x => new BaseCustomerInfoDto
            {
                customerId = x.CustomerId,
                customerSqlId = x.Customer.SqlId,
                customerName = x.Customer.Name
            }).FirstOrDefault();
            if (result == null)
            {
                return new BaseCustomerInfoDto();
            }
            return result;
        }
        public BaseCustomerInfoDto? GetBaseCustomerInfoByOvumDetailId(Guid ovumDetailId)
        {
            return _db.OvumDetails.Where(x => x.OvumDetailId == ovumDetailId).Select(x => new BaseCustomerInfoDto
            {
                birthday = x.CourseOfTreatment.Customer.Birthday,
                customerName = x.CourseOfTreatment.Customer.Name,
                customerSqlId = x.CourseOfTreatment.Customer.SqlId
            }).FirstOrDefault();
        }
    }
}
