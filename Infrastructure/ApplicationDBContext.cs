using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Infrastructure
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }
        internal DbSet<Book> Books { get; set; }
        internal DbSet<Customer> Customers { get; set; }
        internal DbSet<Order> Orders { get; set; }
        
    }
}
