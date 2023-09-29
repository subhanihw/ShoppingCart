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
    }
}
