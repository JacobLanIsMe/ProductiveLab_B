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
        private readonly ReproductiveLabContext _dbContext;
        public CustomerRepository(ReproductiveLabContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Guid? GetCustomerIdByCourseOfTreatmentId(Guid courseOfTreatmentId)
        {
            return _dbContext.CourseOfTreatments.Where(x=>x.CourseOfTreatmentId == courseOfTreatmentId).Select(x=> x.SpermOperationId == (int)GermCellOperationEnum.freeze ? x.CustomerId : x.Customer.Spouse).FirstOrDefault();
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
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }
        public Customer? GetLatestCustomer()
        {
            return _dbContext.Customers.OrderByDescending(x => x.SqlId).FirstOrDefault();
        }
        public Customer? GetCustomerById(Guid customerId)
        {
            return _dbContext.Customers.FirstOrDefault(x => x.CustomerId == customerId);
        }
        public void UpdateSpouse(Customer customer, Guid spouseCustomerId)
        {
            customer.Spouse = spouseCustomerId;
            _dbContext.SaveChanges();
        }

        public List<Gender> GetGenders()
        {
            return _dbContext.Genders.ToList();
        }
        public Customer? GetCustomerBySqlId(int customerSqlId)
        {
            return _dbContext.Customers.FirstOrDefault(x=>x.SqlId == customerSqlId);
        }
    }
}
