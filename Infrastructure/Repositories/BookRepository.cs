
using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private ApplicationDBContext _dbContext;
        public BookRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            if(!_dbContext.Books.Any())
            {
                _dbContext.Books.AddRange(new Book() { ID = 1, Title = "Intoduction to HRM", Authors = "Jason.T, Kotler", Price = 100, QuantityAvailable = 10 },
                 new Book() { ID = 2, Title = "Core Java ", Authors = "John, Jason", Price = 500, QuantityAvailable = 20 },
                 new Book() { ID = 3, Title = "Design Principles ", Authors = "Ramesh, Suresh", Price = 1000, QuantityAvailable = 30 });
                _dbContext.SaveChanges();
            }
            
            
        }

        public bool ValidateBookOrder(List<OrderItem> items)
        {
            bool flag = false;
            var query = (from c in _dbContext.Books.AsEnumerable<Book>()
                        join d in items on c.ID equals d.BookID
                        where c.QuantityAvailable >= d.Quantity
                        select c);
            
            if(query.Count() == items.Count)
            {
                flag = true;
            }
            return true;
        }
        public void UpdateBookQuantity(List<OrderItem> items, bool status)
        {
            var query = from c in _dbContext.Books.AsEnumerable<Book>()
                        join d in items on c.ID equals d.BookID
                        select new { ID=c.ID, Quantity=d.Quantity  };
            List<Book> books = new List<Book>();
            if (status)
            {
                foreach(var x in query)
                {
                    Book book = _dbContext.Books.FirstOrDefault(c => c.ID == x.ID);
                    book.QuantityAvailable = book.QuantityAvailable - x.Quantity;
                    books.Add(book);    
                }
            }
            else
            {
                foreach (var x in query)
                {
                    Book book = _dbContext.Books.FirstOrDefault(c => c.ID == x.ID);
                    book.QuantityAvailable = book.QuantityAvailable + x.Quantity;
                    books.Add(book);
                }
            }
            _dbContext.Books.UpdateRange(books);
        }
    }
}
