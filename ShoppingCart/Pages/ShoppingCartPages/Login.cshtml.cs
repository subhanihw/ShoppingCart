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
            //if (ModelState.IsValid)
            //{
            //    string username = UserL.Username;

            //    List<UserLogin> userLogins = await IRepo.GetUsernamePassword(username);

            //    if (userLogins.Any(u => u.Username == UserL.Username) && userLogins.Any(u => u.Password == UserL.Password))
            //    {

            //        TempData["Message"] = "Login successful!";
            //        return RedirectToPage("/Index");
            //    }
            //    else
            //    {

            //        TempData["Message"] = "Username not found. Please register.";
            //        return RedirectToPage();
            //    }
            //}


            //return Page();

        }
        [BindProperty]
        public UserLogin UserL { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                string username = UserL.Username;

                LoginDto userLogins = await IRepo.GetUsernamePassword(username);
                await Console.Out.WriteLineAsync(userLogins.Password);

                if (userLogins!=null && userLogins.UserName==UserL.Username && userLogins.Password==UserL.Password)
                {

                    TempData["Message"] = "Login successful!";
                    return RedirectToPage("/Index");
                }
                else
                {

                    TempData["Message"] = "Username not found. Please register.";
                    return RedirectToPage("/ShoppingCartPages/Registration");
                }
            }


            return Page();
        }
    }
}
