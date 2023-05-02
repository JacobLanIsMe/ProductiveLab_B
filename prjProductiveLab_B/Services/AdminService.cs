using Microsoft.EntityFrameworkCore;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using ReproductiveLabDB.Models;
using System.Transactions;

namespace prjProductiveLab_B.Services
{
    public class AdminService: IAdminService
    {
        private readonly ReproductiveLabContext dbContext;
        public AdminService(ReproductiveLabContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public BaseResponseDto AddCustomer(AddCustomerDto input)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    Customer customer = new Customer
                    {
                        Name = input.name,
                        GenderId = input.genderId,
                        Birthday = input.birthday,
                    };
                    dbContext.Customers.Add(customer);
                    dbContext.SaveChanges();
                    if (input.spouseName != null && input.spouseGenderId != null && input.spouseBirthday != null)
                    {
                        Guid latestCustomerId = dbContext.Customers.OrderByDescending(x => x.SqlId).Select(x => x.CustomerId).FirstOrDefault();
                        Customer spouse = new Customer
                        {
                            Name = input.spouseName,
                            GenderId = (int)input.spouseGenderId,
                            Birthday = (DateTime)input.spouseBirthday,
                            Spouse = latestCustomerId
                        };
                        dbContext.Customers.Add(spouse);
                        dbContext.SaveChanges();
                        Guid spouseCustomerId = dbContext.Customers.OrderByDescending(x => x.SqlId).Select(x => x.CustomerId).FirstOrDefault();
                        var q = dbContext.Customers.FirstOrDefault(x => x.CustomerId == latestCustomerId);
                        if (q != null)
                        {
                            q.Spouse = spouseCustomerId;
                            dbContext.SaveChanges();
                        }
                    }
                    scope.Complete();
                }
                result.SetSuccess();
            }   
            catch(Exception ex)
            {
                result.SetError(ex.Message);
            }
            return result;
        }
        public async Task<List<CommonDto>> GetGenders()
        {
            return await dbContext.Genders.Select(x => new CommonDto
            {
                id = x.SqlId,
                name = x.Name,
            }).OrderBy(x=>x.id).ToListAsync();
        }
     }
}
