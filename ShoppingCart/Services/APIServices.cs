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

    
    }
}
