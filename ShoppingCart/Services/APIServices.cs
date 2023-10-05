using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ShoppingCart.Models;
using ShoppingCart.Models.DTO;
using System.Net.Http;
using System.Text;

namespace ShoppingCart.Services
{
    public class APIServices : IAPIServices
    {
        private readonly IHttpClientFactory httpClientFactory;
        public APIServices(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task AddUser(RegistrationDto ReDto)
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");

            string jsonContent = JsonConvert.SerializeObject(ReDto);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/Customer/Add", stringContent);
        }
        

        public async Task<LoginDto> GetUsernamePassword(string username)
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            var queryParams = new List<KeyValuePair<string, string>>
             {
                 new KeyValuePair<string, string>("username", username)
             };

            string requestUrl = QueryHelpers.AddQueryString("api/Customer/Validate", queryParams);

            var response = await httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var userLogins = JsonConvert.DeserializeObject<LoginDto>(content);
                return userLogins;
            }
            return null;
        }

        public async Task<List<Product>> GetAll()
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            var response = await httpClient.GetAsync("api/Product");
            if (response.IsSuccessStatusCode)
            {
                var productList = await response.Content.ReadFromJsonAsync<List<Product>>();
                return productList;
            }
            else
                return null;
        }

        public async Task<List<Cart>> GetAllCart()
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            var response = await httpClient.GetAsync("api/Cart");
            if (response.IsSuccessStatusCode)
            {
                var cartList = await response.Content.ReadFromJsonAsync<List<Cart>>();
                return cartList;
            }
            else
                return null;
        }

        public async Task AddToCart(AddToCartDTO addToCart)
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            string jsonContent = JsonConvert.SerializeObject(addToCart);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/Cart/Add", stringContent);
        }

        public async Task<List<CartItemsDTO>> GetCartItems(int UserID)
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
 
            var response = await httpClient.GetAsync($"api/Cart/UserID/{UserID}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cartItems = JsonConvert.DeserializeObject<List<CartItemsDTO>>(content);
                return cartItems;
            }
            return null;
        }

        public async Task<decimal> GetTotalPriceCart(int UserID)
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");

            var response = await httpClient.GetAsync($"api/Cart/TotalPrice/{UserID}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var TotalPrice = JsonConvert.DeserializeObject<TotalPriceDTO>(content);
                return TotalPrice.Total;
            }
            return 0.0m;
        }

        public async Task DeleteCartItem(int UserID, int ProductID)
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            var queryParams = new List<KeyValuePair<string, string>>
             {
                 new KeyValuePair<string, string>("UserID", UserID.ToString()),
                 new KeyValuePair<string, string>("ProductID", ProductID.ToString())
             };

            string requestUrl = QueryHelpers.AddQueryString("api/Cart/Delete", queryParams);

            await httpClient.DeleteAsync(requestUrl);
        }
    }
}
