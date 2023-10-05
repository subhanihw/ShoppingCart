using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Models.DTO;
using ShoppingCart.API.Repositories;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IRepository repository;

        public CartController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItems()
        {
            var cartItems = await repository.GetCartsAsync();
            return Ok(cartItems);
        }

        [HttpGet]
        [Route("UserID/{id}")]
        public async Task<IActionResult> GetCartItemByUserID([FromRoute] int id)
        {
            var cartItem = await repository.GetCartByUserIdAsync(id);
            if (cartItem == null)
            {
                return NotFound($"Cart item with ID = {id} not found");
            }
            return Ok(cartItem);
        }

        [HttpGet]
        [Route("TotalPrice/{id}")]
        public async Task<IActionResult> GetTotalPriceByUserID([FromRoute] int id)
        {
            var totalPrice = await repository.GetTotalPrice(id);
            TotalPriceDTO dto = new TotalPriceDTO { UserID = id , Total = totalPrice};
            return Ok(dto);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddCartItem(CartDTO cartItem)
        {
            var newCartItem = await repository.AddCart(cartItem);
            return Ok(newCartItem);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteCartItem([FromQuery] int UserID, [FromQuery] int ProductID)
        {
            await repository.DeleteCartByIdAsync(UserID, ProductID);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCartItem([FromRoute] int id, [FromQuery] int Quantity)
        {
            var updatedCartItem = await repository.UpdateCartItemQuantityByIdAsync(id, Quantity);

            if (updatedCartItem != null)
            {
                return Ok(updatedCartItem);
            }
            return NotFound($"Cart item with ID = {id} not found");
        }
    }
}
