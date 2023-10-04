using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public interface IApiService
    {
        Task<List<Customer>> GetAll();
    }
}
