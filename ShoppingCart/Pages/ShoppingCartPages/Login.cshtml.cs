
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShoppingCart.Models;
using ShoppingCart.Models.DTO;
using ShoppingCart.Services;
using System.ComponentModel.DataAnnotations;

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

            //ModelState.Clear();


        }

        [BindProperty]
        public UserLogin UserL { get; set; } = new UserLogin();


        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {


                //string username = UserL.Username;

                //LoginDto userLogins = await IRepo.GetUsernamePassword(username);


                //if (userLogins != null && userLogins.UserName == UserL.Username)
                //{

                //    if(userLogins.Password == UserL.Password)
                //    {
                //        TempData["UserID"] = userLogins.UserID;
                //        return RedirectToPage("/ProductsPage");
                //    }

                //}

                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Invalid username or Password. Please Register");
                //    return Page();
                //}
                string username = UserL.Username;
                string password = UserL.Password;

                var userLogins = await IRepo.GetUsernameAndPassword(username, password);
                if (userLogins)
                {
                    //TempData["UserID"] = userLogins.UserID;

                    return RedirectToPage("/ProductsPage");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or Password. Please Register");
                    return Page();
                }
            }


            return Page();
        }
    }
}

