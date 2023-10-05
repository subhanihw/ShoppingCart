using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Models.DTO;
using ShoppingCart.API.Repositories;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository repository;

        public OrderController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> InsertOrder(OrderDTO order)
        {
            var newOrder = await repository.InsertOrders(order);
            return Ok(newOrder);
        }

        [HttpPost]
        [Route("OrderDetails/Add")]
        public async Task<IActionResult> InsertOrderDetails(OrderDetailDTO order)
        {
            var orderDetails = await repository.InsertOrderDetail(order);
            return Ok(orderDetails);
        }
    }
}
