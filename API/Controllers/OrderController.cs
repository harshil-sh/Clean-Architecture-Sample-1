using Application.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public ActionResult<Order> PlaceOrder(Order order)
        {
            return Ok(_orderService.PlaceOrder(order));
        }
        [HttpGet]
        public ActionResult<Order> CancelOrder(long id)
        {
            return Ok(_orderService.CancelOrder(id));
        }
    }
}
