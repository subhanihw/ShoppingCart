
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;
using System.Text.Json;
using System.Text;
using ShoppingCart.Models.DTO;

using System.Net.Http;
using ShoppingCart.Services;
//using Microsoft.Data.SqlClient;

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


        public async Task<IActionResult> OnPostAsync(ExceptionResponseDto exceptionResponseDto)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var dto = new RegistrationDto()
                    {
                        
                        PhoneNumber = User.PhoneNumber,
                        State = User.State,
                        Gender = User.Gender,
                        Password = User.Password,
                        Username = User.Username,
                    };
                    await IRepo.AddUser(dto, exceptionResponseDto);
                    if (string.IsNullOrEmpty(exceptionResponseDto.Exception))
                    {
                        TempData["RegistrationSuccess"] = true; // Set a flag to indicate successful registration
                        TempData["Success"] = "Registration Success. Please login";
                        return RedirectToPage("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("User.Username", "Username is already in use. Please choose a different username.");
                    }

                }
                catch (HttpRequestException ex)
                {
                    // Handle other exceptions as needed
                    ModelState.AddModelError(string.Empty, "An error occurred during registration.");

                }

            }
            return Page();

        }
        public enum IndianState

        {

            AndhraPradesh,

            ArunachalPradesh,
            Assam,
            Bihar,
            Chhattisgarh,
            Goa,
            Gujarat,
            Haryana,
            Himachal, Jharkhand, Karnataka, Kerala,
            MadhyaPradesh, Maharashtra, Manipur, Meghalaya, Mizoram, Nagaland, Odisha, Punjab, Rajasthan,
            Sikkim, TamilNadu, Telangana, Tripura, UttarPradesh, Uttarakhand, WestBengal


        }

    }
}
