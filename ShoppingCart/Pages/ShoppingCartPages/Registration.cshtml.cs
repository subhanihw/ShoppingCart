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

            IRepo.AddUser(reg);

            
            return RedirectToPage("Login");

        }

    }
}
