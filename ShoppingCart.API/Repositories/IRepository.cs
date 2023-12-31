﻿using ShoppingCart.API.Models;
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
        Task<bool> ValidateUserNamePassword(string userName, string password);

        // Product Methods
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProduct(ProductDTO product);
        Task<Product> DeleteProductAsync(int id);
        Task<Product> UpdateProductAsync(int id, ProductDTO product);

        // Cart methods
        Task<List<Cart>> GetCartsAsync();
        Task<List<CartItemsDTO>> GetCartByUserIdAsync(int id);
        Task<Cart> AddCart(CartDTO cart);
        Task<int> DeleteCartByIdAsync(int UserID, int ProductID);
        Task<Cart> GetCartByIdAsync(int id);
        Task<decimal> GetTotalPrice(int userID);
        Task DeleteCart(int userID);

        // Order methods
        Task<Order> InsertOrders(OrderDTO order);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);

        // Order Details method
        Task<OrderDetails> InsertOrderDetail(OrderDetailDTO orderDetail);
        Task<List<OrderProductsDTO>> GetProductDetails(int userId, int orderId);

    }
}
