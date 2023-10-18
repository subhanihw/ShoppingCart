using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.ExceptionHandling;
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
            try
            {
                var newOrder = await repository.InsertOrders(order);
                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("OrderDetails/Add")]
        public async Task<IActionResult> InsertOrderDetails(OrderDetailDTO order)
        {
            try
            {
                var orderDetails = await repository.InsertOrderDetail(order);
                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrdersByUserID(int id)
        {
            try
            {
                var orders = await repository.GetOrdersByUserIdAsync(id);
                return Ok(orders);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetProductDetails")]
        public async Task<IActionResult> GetProductDetailsByOrderID([FromQuery] int userID,[FromQuery] int orderID)
        {
            try
            {
                var products = await repository.GetProductDetails(userID, orderID);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
