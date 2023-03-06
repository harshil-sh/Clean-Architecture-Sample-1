using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookRepository _bookRepository;
        public OrderRepository(ApplicationDBContext applicationDBContext,ICustomerRepository customerRepository, IBookRepository bookRepository)
        {
            _dbContext = applicationDBContext;
            _customerRepository = customerRepository;
            _bookRepository = bookRepository;
        }

        public List<OrderItem> Calculate(Order order)
        {
            List<OrderItem> items = new List<OrderItem>();  
            foreach (var item in order.Items)
            {
                item.TotalPrice = item.Quantity*item.Price;
                items.Add(item);
            }
            return items;
        }

        public Order CancelOrder(long id)
        {
            var order = _dbContext.Orders.FirstOrDefault(c=>c.Id == id);
            if (order != null)
            {
                order.Status = false;
                TimeSpan ts = order.OrderDeliveryDate - DateTime.Now;
                if(ts.Days<=3 && ts.Days>0)
                {
                    order.RefundAmount = order.TotalPrice - (order.TotalPrice * 0.20f);
                }
                else if(ts.Days>0)
                {
                    order.RefundAmount = order.TotalPrice;
                }
                else
                {
                    order.RefundAmount = 0;
                }
                order.RefundAmount= order.TotalPrice;
                _dbContext.Orders.Update(order);
                _dbContext.SaveChangesAsync();
                _bookRepository.UpdateBookQuantity(order.Items, false);
                return order;
            }
            else
            {
                throw new Exception("Invalid Order ID");
            }

        }

        public long GenerateOrderId()
        {
            if (_dbContext.Orders.Any())
            {
                return _dbContext.Orders.Max(c => c.Id) + 1;
            }
            else
                return 1;
            
        }

        public Order PlaceOrder(Order order)
        {
            var customer = _customerRepository.GetAllCustomers(order.CustomerID);
            if (customer == null)
            {
                throw new Exception("Customer NotFound");
            }

            order.Id = GenerateOrderId();
            order.Status = true;
            order.OrderDeliveryDate=DateTime.Now.AddDays(7);
            //var items = order.Items.Select(c=>c.BookID).ToList();
            var isValidBookitems = _bookRepository.ValidateBookOrder(order.Items); 
            if(!isValidBookitems)
            {
                throw new Exception("Invalid Book Information");
            }
            //var isValidBookItems = _dbContext.Books.Where(c=>items.Contains(c.ID)).ToList();
            //if(isValidBookItems.Count()!=items.Count)
            //{
            //    throw new Exception("Invalid Book Information");
            //}
            order.Status=true;
            List<OrderItem> orderItems = order.Items;
            order.Items=Calculate(order);
            order.TotalNoOfItems = order.Items.Sum(c => c.Quantity);
            order.TotalPrice = order.Items.Sum(c => c.TotalPrice);
            order.OrderDateTime = DateTime.Now;
            _dbContext.Orders.Add(order);
            _dbContext.SaveChangesAsync();
            _bookRepository.UpdateBookQuantity(order.Items,true);
            return order;
        }
    }
}
