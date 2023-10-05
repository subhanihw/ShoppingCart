using ShoppingCart.API.Models;
using System.Data;
using Dapper;
using ShoppingCart.API.Models.DTO;
using System.Data.Common;
using System.Data.SqlTypes;

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
            var customer = await GetCustomerByIdAsync(id);
            if (customer == null)
                return null;
            var param = new DynamicParameters();
            param.Add("@CustomerID", id, DbType.Int32);
            await _connection.ExecuteAsync("DeleteCustomer", param, commandType: CommandType.StoredProcedure);
            return customer;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@CustomerID", id, DbType.Int32);
            var customer = await _connection.QueryFirstOrDefaultAsync<Customer>
                              ("GetCustomerById", param, commandType: CommandType.StoredProcedure);
            return customer;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var customers = await _connection.QueryAsync<Customer>
                              ("GetAllCustomers", null, commandType: CommandType.StoredProcedure);
            return customers.ToList();
        }


        public async Task<Customer> UpdateCustomerAsync(int id, CustomerDTO customer)
        {
            var Cust = await GetCustomerByIdAsync(id);
            if (Cust == null)
                return null;
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

        public async Task<ValidateDTO> GetPasswordByUserNameAsync(string userName)
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
        }

        // Product Method Implementations
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@ProductID", id, DbType.Int32);
            var product = await _connection.QueryFirstOrDefaultAsync<Product>
                              ("GetProductById", param, commandType: CommandType.StoredProcedure);
            return product;

        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var Products = await _connection.QueryAsync<Product>
                              ("GetAllProducts", null, commandType: CommandType.StoredProcedure);
            return Products.ToList();
        }

        public async Task<Product> AddProduct(ProductDTO product)
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
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var Product = await GetProductByIdAsync(id);
            if (Product == null)
                return null;
            var param = new DynamicParameters();
            param.Add("@ProductID", id, DbType.Int32);
            await _connection.ExecuteAsync("DeleteProduct", param, commandType: CommandType.StoredProcedure);
            return Product;
        }

        public async Task<Product> UpdateProductAsync(int id, ProductDTO product)
        {
            var Product = await GetProductByIdAsync(id);
            if (Product == null)
                return null;
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

   
        // Cart methods implementation
        public async Task<List<Cart>> GetCartsAsync()
        {
            var carts = await _connection.QueryAsync<Cart>("GetAllCartItems", null, commandType: CommandType.StoredProcedure);
            return carts.ToList();
        }

        public async Task<List<CartItemsDTO>> GetCartByUserIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@UserID", id, DbType.Int32);
            var cart = await _connection.QueryAsync<CartItemsDTO>(
                "GetCartProductsByUserID",
                param,
                commandType: CommandType.StoredProcedure
            );
            return cart.ToList();
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@CartID", id, DbType.Int32);
            var cart = await _connection.QueryFirstOrDefaultAsync<Cart>(
                "GetCartItemById",
                param,
                commandType: CommandType.StoredProcedure
            );
            return cart;
        }

        public async Task<Cart> AddCart(CartDTO cart)
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
        }

        public async Task<Cart> UpdateCartItemQuantityByIdAsync(int id, int Quantity)
        {
            var existingCart = await GetCartByIdAsync(id);
            if (existingCart == null)
                return null;

            var parameters = new DynamicParameters();
            parameters.Add("@CartID", id, DbType.Int32);
            parameters.Add("Quantity", Quantity, DbType.Int32);

            await _connection.ExecuteAsync("UpdateCartItemQuantity", parameters, commandType: CommandType.StoredProcedure);

            var updatedCart = await GetCartByIdAsync(id);
            return updatedCart;
        }

        public async Task DeleteCartByIdAsync(int UserID, int ProductID)
        {
            var param = new DynamicParameters();
            param.Add("@UserID", UserID, DbType.Int32);
            param.Add("@ProductID", ProductID, DbType.Int32);
            await _connection.ExecuteAsync("DeleteCartItem", param, commandType: CommandType.StoredProcedure);
        }


        public async Task<decimal> GetTotalPrice(int userID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", userID, DbType.Int32);
            parameters.Add("@TotalPrice", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 10, scale: 2);

            await _connection.ExecuteAsync("TotalPriceByUserID", parameters, commandType: CommandType.StoredProcedure);

            
            var totalPrice = parameters.Get<decimal>("@TotalPrice");
            return totalPrice;
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

    }
}
