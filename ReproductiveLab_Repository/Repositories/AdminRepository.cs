﻿using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos;
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
    public class AdminRepository : IAdminRepository
    {
        private readonly ReproductiveLabContext _dbContext;
        public AdminRepository(ReproductiveLabContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
