using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.ExceptionHandling;
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
            try
            {
                var cartItems = await repository.GetCartsAsync();
                return Ok(cartItems);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("UserID/{UserID}")]
        public async Task<IActionResult> GetCartItemByUserID([FromRoute] int UserID)
        {
            try
            {
                var cartItem = await repository.GetCartByUserIdAsync(UserID);
                if (cartItem.Count == 0)
                {
                    return Ok("Cart is Empty");
                }
                return Ok(cartItem);
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
        [Route("TotalPrice/{UserID}")]
        public async Task<IActionResult> GetTotalPriceByUserID([FromRoute] int UserID)
        {
            try
            {
                var totalPrice = await repository.GetTotalPrice(UserID);
                if (totalPrice == 0.0m)
                    return Ok("Cart is Empty");
                TotalPriceDTO dto = new TotalPriceDTO { UserID = UserID, Total = totalPrice };
                return Ok(dto);
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

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddCartItem(CartDTO cartItem)
        {
            try
            {
                var newCartItem = await repository.AddCart(cartItem);
                return Ok(newCartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteCartItem([FromQuery] int UserID, [FromQuery] int ProductID)
        {
            try
            {
                await repository.DeleteCartByIdAsync(UserID, ProductID);
                return Ok();
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

        [HttpDelete]
        [Route("DeleteAllCartItems/{UserID}")]
        public async Task<IActionResult> DeleteAll([FromRoute] int UserID)
        {
            try
            {
                await repository.DeleteCart(UserID);
                return Ok();
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
    }
}
