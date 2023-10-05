using ShoppingCart.API.Models;
using ShoppingCart.API.Models.DTO;

namespace ShoppingCart.API.Repositories
{
    public interface IRepository
    {
        // Customer Methods
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> AddCustomer(CustomerDTO customer);
        Task<Customer> DeleteCustomerAsync(int id);
        Task<Customer> UpdateCustomerAsync(int id, CustomerDTO customer);
        Task<ValidateDTO> GetPasswordByUserNameAsync(string userName);

        // Product Methods
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProduct(ProductDTO product);
        Task<Product> DeleteProductAsync(int id);
        Task<Product> UpdateProductAsync(int id, ProductDTO product);

        // Category methods
        Task<Category> AddCategory(CategoryDTO category);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> UpdateCategoryAsync(int id, CategoryDTO category);
        Task<Category> DeleteCategoryAsync(int id);

        // Cart methods
        Task<List<Cart>> GetCartsAsync();
        Task<List<CartItemsDTO>> GetCartByUserIdAsync(int id);
        Task<Cart> AddCart(CartDTO cart);
        Task<Cart> UpdateCartItemQuantityByIdAsync(int id, int Quantity);
        Task DeleteCartByIdAsync(int UserID, int ProductID);
        Task<Cart> GetCartByIdAsync(int id);
        Task<decimal> GetTotalPrice(int userID);

        // Order methods
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(OrderDTO order);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
        Task<Order> UpdateOrderStatusAsync(int id, string status);
        Task<Order> DeleteOrderAsync(int id);

        
    }
}
