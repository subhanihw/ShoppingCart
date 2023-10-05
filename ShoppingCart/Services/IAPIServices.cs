using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Models.DTO;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    public interface IAPIServices
    {
        Task<List<Product>> GetAll();
        Task<List<Cart>> GetAllCart();
        Task AddUser(RegistrationDto ReDto);
        Task<LoginDto> GetUsernamePassword(string username);
        Task AddToCart(AddToCartDTO addToCart);

        Task<List<CartItemsDTO>> GetCartItems(int UserID);
        Task<decimal> GetTotalPriceCart(int UserID);
        Task DeleteCartItem(int UserID, int ProductID);
        
    }
}
