using ShoppingCart.API.Models;
using System.Data;
using Dapper;
using ShoppingCart.API.Models.DTO;
using System.Data.Common;
using System.Data.SqlTypes;
using ShoppingCart.API.ExceptionHandling;

namespace ShoppingCart.API.Repositories
{
    public class SqlClientClass : IRepository
    {
        private readonly IDbConnection _connection;

        public SqlClientClass(IDbConnection dbConnection)
        {
            this._connection = dbConnection;
        }

        public async Task<Customer> AddCustomer(CustomerDTO customer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Username", customer.Username);
            parameters.Add("Password", customer.Password);
            parameters.Add("Gender", customer.Gender);
            parameters.Add("PhoneNumber", customer.PhoneNumber);
            parameters.Add("State", customer.State);
            parameters.Add("NewCustomerID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await _connection.ExecuteAsync("CreateCustomer", parameters, commandType: CommandType.StoredProcedure);
            int InsertedCustomerID = parameters.Get<int>("@NewCustomerID");
            var NewCustomer = await GetCustomerByIdAsync(InsertedCustomerID);
            return NewCustomer;
        }

        public async Task<Customer> DeleteCustomerAsync(int id)
        {
            try
            {
                var customer = await GetCustomerByIdAsync(id);
                if (customer == null)
                   throw new NotFoundException("Customer not found.");
                var param = new DynamicParameters();
                param.Add("@CustomerID", id, DbType.Int32);
                await _connection.ExecuteAsync("DeleteCustomer", param, commandType: CommandType.StoredProcedure);
                return customer;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CustomerID", id, DbType.Int32);
                var customer = await _connection.QueryFirstOrDefaultAsync<Customer>
                    ("GetCustomerById", param, commandType: CommandType.StoredProcedure);
                if (customer == null)
                    throw new NotFoundException("Customer not found.");
                return customer;
                
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            try
            {
                var customers = await _connection.QueryAsync<Customer>
                                  ("GetAllCustomers", null, commandType: CommandType.StoredProcedure);
                return customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<Customer> UpdateCustomerAsync(int id, CustomerDTO customer)
        {
            try
            {
                var Cust = await GetCustomerByIdAsync(id);
                if (Cust == null)
                    throw new NotFoundException("Customer not found.");
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerID", id, DbType.Int32);
                parameters.Add("Username", customer.Username);
                parameters.Add("Password", customer.Password);
                parameters.Add("Gender", customer.Gender);
                parameters.Add("PhoneNumber", customer.PhoneNumber);
                parameters.Add("State", customer.State);

                await _connection.ExecuteAsync("UpdateCustomer", parameters, commandType: CommandType.StoredProcedure);
                Cust = await GetCustomerByIdAsync(id);
                return Cust;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ValidateDTO> GetPasswordByUserNameAsync(string userName)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@InputUsername", userName, DbType.String, ParameterDirection.Input);
                parameters.Add("@OutputPassword", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                parameters.Add("@OutputCustomerID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await _connection.ExecuteAsync("GetPasswordByUsername", parameters, commandType: CommandType.StoredProcedure);
                string password = parameters.Get<string>("@OutputPassword");
                int userID = parameters.Get<int>("@OutputCustomerID");
                var validateDTO = new ValidateDTO
                {
                    UserID = userID,
                    UserName = userName,
                    password = password
                };
                return validateDTO;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Product Method Implementations
        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ProductID", id, DbType.Int32);
                var product = await _connection.QueryFirstOrDefaultAsync<Product>
                                  ("GetProductById", param, commandType: CommandType.StoredProcedure);
                if (product == null) { 
                    throw new NotFoundException("Product Not Found"); 
                }
                return product;
            }catch (NotFoundException ex) {
                throw new NotFoundException(ex.Message);
            }    
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var Products = await _connection.QueryAsync<Product>
                                  ("GetAllProducts", null, commandType: CommandType.StoredProcedure);
                return Products.ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> AddProduct(ProductDTO product)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Name", product.Name, DbType.String);
                parameters.Add("Description", product.Description, DbType.String);
                parameters.Add("Price", product.Price, DbType.Decimal);
                parameters.Add("Stock", product.Stock, DbType.Int32);
                parameters.Add("ImageURL", product.ImageURL, DbType.String);
                parameters.Add("CategoryID", product.CategoryID, DbType.Int32);
                parameters.Add("NewProductID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await _connection.ExecuteAsync("CreateProduct", parameters, commandType: CommandType.StoredProcedure);
                int InsertedProductID = parameters.Get<int>("@NewProductID");
                var NewProduct = await GetProductByIdAsync(InsertedProductID);
                return NewProduct;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            try
            {
                var Product = await GetProductByIdAsync(id);
                if (Product == null)
                    throw new NotFoundException("Product Not Found");
                var param = new DynamicParameters();
                param.Add("@ProductID", id, DbType.Int32);
                await _connection.ExecuteAsync("DeleteProduct", param, commandType: CommandType.StoredProcedure);
                return Product;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> UpdateProductAsync(int id, ProductDTO product)
        {
            try
            {
                var Product = await GetProductByIdAsync(id);
                if (Product == null)
                    throw new NotFoundException("Product Not Found");
                var parameters = new DynamicParameters();
                parameters.Add("@ProductID", id, DbType.Int32);
                parameters.Add("Name", product.Name, DbType.String);
                parameters.Add("Description", product.Description, DbType.String);
                parameters.Add("Price", product.Price, DbType.Decimal);
                parameters.Add("Stock", product.Stock, DbType.Int32);
                parameters.Add("ImageURL", product.ImageURL, DbType.String);
                parameters.Add("CategoryID", product.CategoryID, DbType.Int32);

                await _connection.ExecuteAsync("UpdateProduct", parameters, commandType: CommandType.StoredProcedure);
                Product = await GetProductByIdAsync(id);
                return Product;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Cart methods implementation
        public async Task<List<Cart>> GetCartsAsync()
        {
            try
            {
                var carts = await _connection.QueryAsync<Cart>("GetAllCartItems", null, commandType: CommandType.StoredProcedure);
                return carts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CartItemsDTO>> GetCartByUserIdAsync(int id)
        {
            try
            {
                var userExists = await GetCustomerByIdAsync(id);
                var param = new DynamicParameters();
                param.Add("@UserID", id, DbType.Int32);
                var cart = await _connection.QueryAsync<CartItemsDTO>(
                    "GetCartProductsByUserID",
                    param,
                    commandType: CommandType.StoredProcedure
                );
                return cart.ToList();
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CartID", id, DbType.Int32);
                var cart = await _connection.QueryFirstOrDefaultAsync<Cart>(
                    "GetCartItemById",
                    param,
                    commandType: CommandType.StoredProcedure
                );
                if (cart == null)
                    throw new NotFoundException("cart ID not found.");
                return cart;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cart> AddCart(CartDTO cart)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserID", cart.UserID, DbType.Int32);
                parameters.Add("ProductID", cart.ProductID, DbType.Int32);
                parameters.Add("Quantity", cart.Quantity, DbType.Int32);
                parameters.Add("NewCartID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync("CreateCartItem", parameters, commandType: CommandType.StoredProcedure);

                int newCartID = parameters.Get<int>("NewCartID");
                var newCart = await GetCartByIdAsync(newCartID);
                return newCart;
            }catch (Exception ex) { 
                throw new Exception(ex.Message ); 
            }
        }


        public async Task<int> DeleteCartByIdAsync(int UserID, int ProductID)
        {
            try
            {
                var IsUserExists = await GetCustomerByIdAsync(UserID);
                var param = new DynamicParameters();
                param.Add("@UserID", UserID, DbType.Int32);
                param.Add("@ProductID", ProductID, DbType.Int32);
                var rowsAffected = await _connection.ExecuteAsync("DeleteCartItem", param, commandType: CommandType.StoredProcedure);

                if (rowsAffected == 0)
                    throw new NotFoundException("No product with given id existed in the cart");
                return rowsAffected;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<decimal> GetTotalPrice(int userID)
        {
            try
            {
                var userExists = await GetCustomerByIdAsync(userID);
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userID, DbType.Int32);
                parameters.Add("@TotalPrice", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 10, scale: 2);

                await _connection.ExecuteAsync("TotalPriceByUserID", parameters, commandType: CommandType.StoredProcedure);


                var totalPrice = parameters.Get<dynamic>("@TotalPrice");
                if (totalPrice == null)
                    return 0.0m;
                return totalPrice;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> InsertOrders(OrderDTO order)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", order.UserID);
            parameters.Add("@OrderDate", order.OrderDate);
            parameters.Add("@Total", order.Total);
            parameters.Add("@OrderID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _connection.ExecuteAsync("InsertOrder", parameters, commandType: CommandType.StoredProcedure);

            int newID = parameters.Get<int>("@OrderID");
            var newOrder = new Order
            {
                OrderID = newID,
                OrderDate = order.OrderDate,
                Total = order.Total,
                UserID = order.UserID
            };

            return newOrder;
        }

        public async Task<OrderDetails> InsertOrderDetail(OrderDetailDTO orderDetail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderID", orderDetail.OrderID);
            parameters.Add("@ProductID", orderDetail.ProductID);
            parameters.Add("@Quantity", orderDetail.Quantity);
            parameters.Add("@Price", orderDetail.Price);
            parameters.Add("@OrderDetailID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _connection.ExecuteAsync("InsertOrderDetail", parameters, commandType: CommandType.StoredProcedure);

            int newOrderDetailID = parameters.Get<int>("@OrderDetailID");

            var orderDetails = new OrderDetails
            {
                OrderDetailID = newOrderDetailID,
                OrderID = orderDetail.OrderID,
                ProductID = orderDetail.ProductID,
                Quantity = orderDetail.Quantity,
                Price = orderDetail.Price
            };
            return orderDetails;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var param = new DynamicParameters();
            param.Add("@UserID", userId, DbType.Int32);

            var orders = await _connection.QueryAsync<Order>("GetOrdersByUserID", param,commandType: CommandType.StoredProcedure);

            return orders.ToList();
        }

        public async Task<List<OrderProductsDTO>> GetProductDetails(int userId, int orderId)
        {
            var param = new DynamicParameters();
            param.Add("@UserID", userId, DbType.Int32);
            param.Add("@OrderID", orderId, DbType.Int32);

            var productDetails = await _connection.QueryAsync<OrderProductsDTO>("GetProductDetails",param,commandType: CommandType.StoredProcedure);

            return productDetails.ToList();
        }

        public async Task<bool> ValidateUserNamePassword(string userName, string password)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", userName);
                parameters.Add("@Password", password);
                parameters.Add("@UserExists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync(
                    "CheckCustomerLogin",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var userExists = parameters.Get<bool>("@UserExists");

                return userExists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCart(int id)
        {
            try{
                var customer = await GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    throw new NotFoundException("Customer not found.");
                }

                var param = new DynamicParameters();
                param.Add("@UserID", id, DbType.Int32);
                await _connection.ExecuteAsync("DeleteAllCartItems", param, commandType: CommandType.StoredProcedure);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
