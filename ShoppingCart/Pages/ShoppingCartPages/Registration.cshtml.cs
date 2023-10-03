using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;
using System.Text.Json;
using System.Text;
using ShoppingCart.Models.DTO;

using System.Net.Http;
using ShoppingCart.Services;

namespace ShoppingCart.Pages.ShoppingCartPages
{
    public class RegistrationModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IAPIServices IRepo;

        public RegistrationModel(IHttpClientFactory httpClientFactory, IAPIServices IRepo)
        {
            this.httpClientFactory = httpClientFactory;
            this.IRepo = IRepo;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public UserReg User { get; set; }

        public async Task<IActionResult> OnPostAsync([Bind("Username,Password,Gender,PhoneNumber,State")] RegistrationDto reg)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("Login");
            }

            IRepo.AddEmployee(reg);

            // Here, you can add code to save the user registration data to a database
            // You can use Entity Framework or Dapper to interact with the database
            //var name=
            //var client = httpClientFactory.CreateClient();
            //var httpRequestMessage = new HttpRequestMessage()
            //{
            //    Method = HttpMethod.Post,
            //    RequestUri = new Uri("https://localhost:7265/api/Customer"),
            //    Content = new StringContent(JsonSerializer.Serialize(reg), Encoding.UTF8, "application/json")
            //};
            //var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            ////httpResponseMessage.EnsureSuccessStatusCode();
            //var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegistrationDto>();
            //if (response is not null)
            //{
            //    return RedirectToAction("Index", "Region");
            //}
            //return RedirectToPage("Index");

            ////User.Username=name.ToString();


            //// Redirect to a confirmation page or login page
            return RedirectToPage("Login");

        }

    }
}
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using ShoppingCart.Models;
//using ShoppingCart.Models.DTO;
//using System;
//using System.Net.Http;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace ShoppingCart.Pages.ShoppingCartPages
//{
//    public class RegistrationModel : PageModel
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public RegistrationModel(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        public void OnGet()
//        {
//            // Handle GET requests here if needed
//        }

//        [BindProperty]
//        public UserReg User { get; set; }


//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return RedirectToPage("Login");
//            }

//            try
//            {
//                var client = _httpClientFactory.CreateClient();
//                var apiUrl = "https://localhost:7265/api/Customer";

//                // Serialize the UserReg object to JSON
//                var jsonContent = JsonSerializer.Serialize(User);

//                var httpRequestMessage = new HttpRequestMessage()
//                {
//                    Method = HttpMethod.Post,
//                    RequestUri = new Uri(apiUrl),
//                    Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
//                };

//                var httpResponseMessage = await client.SendAsync(httpRequestMessage);

//                // Check if the response is successful (status code 200-299)
//                if (httpResponseMessage.IsSuccessStatusCode)
//                {
//                    // Deserialize the response JSON into a RegistrationDto object
//                    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

//                    if (!string.IsNullOrWhiteSpace(responseContent))
//                    {
//                        var response = JsonSerializer.Deserialize<RegistrationDto>(responseContent);

//                        if (response != null)
//                        {
//                            // Redirect to a success page (e.g., Index in this example)
//                            return RedirectToAction("Index", "Region");
//                        }
//                    }
//                }
//                else
//                {
//                    // Handle non-successful responses here
//                    // You might want to add logging or error handling
//                }
//            }
//            catch (Exception ex)
//            {
//                // Handle exceptions here
//                // You might want to add logging or error handling
//            }

//            // Redirect to a confirmation page or login page
//            return RedirectToPage("Registration");
//        }

//    }
//}
