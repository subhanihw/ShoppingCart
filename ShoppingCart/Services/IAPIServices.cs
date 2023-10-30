
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
        Task AddUser(RegistrationDto ReDto, ExceptionResponseDto exceptionResponseDto);
        Task<LoginDto> GetUsernamePassword(string password);
        Task<bool> GetUsernameAndPassword(string username, string password);
        Task AddToCart(AddToCartDTO addToCart);

        Task<List<CartItemsDTO>> GetCartItems(int UserID);
        Task<decimal> GetTotalPriceCart(int UserID);
        Task DeleteCartItem(int UserID, int ProductID);

        Task<int> InsertOrder(OrderDTO order);
        Task InsertOrderDetails(OrderDetailDTO orderDetail);
        Task<List<Order>> GetOrderByUserID(int UserID);
        Task<List<OrderProductDTO>> GetProductDetails(int UserID, int OrderID);

    }
}

