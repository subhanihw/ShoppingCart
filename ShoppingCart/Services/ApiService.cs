using ShoppingCart.Models;
using System.Net.Http;

namespace ShoppingCart.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<List<Customer>> GetAll()
        {
            var httpClient = httpClientFactory.CreateClient("WebAPI");
            var response = await httpClient.GetAsync("api/Customer");
            if (response.IsSuccessStatusCode)
            {
                var customerList = await response.Content.ReadFromJsonAsync<List<Customer>>();
                return customerList;
            }
            else
                return null;
        }
    }
}
