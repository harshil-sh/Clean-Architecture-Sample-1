using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IOrderRepository
    {
        public Order PlaceOrder(Order order);
        public long GenerateOrderId();

        public Order CancelOrder(long id);

        public List<OrderItem> Calculate(Order order);
    }
}
