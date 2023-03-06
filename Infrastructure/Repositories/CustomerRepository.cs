using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        

        private readonly ApplicationDBContext _dbContext;
        public CustomerRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            if(!dbContext.Customers.Any())
            {
                _dbContext.Customers.AddRange(new Customer() { ID = 1, FullName = "Harshil Shah", EmailID = "Harshil.sh@gmail.com" },
                    new Customer() { ID = 2, FullName = "Riddhi Shah", EmailID = "RiddhiShah@gmail.com" },
                    new Customer() { ID = 3, FullName = "Jason Statum", EmailID = "Jason.T@gmail.com" });
                _dbContext.SaveChanges();
            }
        }

        public Task<Customer> GetAllCustomers(long id)
        {
            return _dbContext.Customers.FirstOrDefaultAsync(c=>c.ID==id);
        }
    }
}
