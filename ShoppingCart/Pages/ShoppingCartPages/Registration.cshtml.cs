using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;

namespace ShoppingCart.Pages.ShoppingCartPages
{
    public class RegistrationModel : PageModel
    {
        public void OnGet()
        {
        }
        [BindProperty]
        public UserReg User { get; set; }
        public IActionResult OnPost([Bind("Username,Password,Gender,PhoneNumber,State")] UserReg reg)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("Login");
            }

            // Here, you can add code to save the user registration data to a database
            // You can use Entity Framework or Dapper to interact with the database
            //var name=
            //IRepo.Add(reg);

            //User.Username=name.ToString();

            // Redirect to a confirmation page or login page
            return RedirectToPage("Registration");

        }
    }
}
