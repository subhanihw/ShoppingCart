using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;
using ShoppingCart.Models.DTO;
using ShoppingCart.Services;

namespace ShoppingCart.Pages.ShoppingCartPages
{
    public class LoginModel : PageModel
    {
        private readonly IAPIServices IRepo;

        public LoginModel(IAPIServices IRepo)
        {
            this.IRepo = IRepo;
        }
        
        public void OnGet()
        {

        }
        [BindProperty]
        public UserLogin UserL { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                string username = UserL.Username;

                LoginDto userLogins = await IRepo.GetUsernamePassword(username);
                

                if (userLogins != null && userLogins.UserName == UserL.Username)
                {
                
                    if(userLogins.Password == UserL.Password)
                    {
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Password. Please try again");
                        return Page();
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or Please Register");
                    return Page();
                }
                
            }


            return Page();
        }
    }
}
