using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;

namespace ShoppingCart.Pages.ShoppingCartPages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }
        [BindProperty]
        public UserLogin UserL { get; set; }
    }
}
