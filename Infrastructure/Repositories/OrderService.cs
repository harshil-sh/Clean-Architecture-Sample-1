using Application.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<OrderItem> Calculate(Order order)
        {
            return _orderRepository.Calculate(order);
        }

        public Order CancelOrder(long id)
        {
            return _orderRepository.CancelOrder(id);
        }

        public long GenerateOrderId()
        {
            return _orderRepository.GenerateOrderId();
        }

        public Order PlaceOrder(Order order)
        {
            return _orderRepository.PlaceOrder(order);
        }
    }
}
