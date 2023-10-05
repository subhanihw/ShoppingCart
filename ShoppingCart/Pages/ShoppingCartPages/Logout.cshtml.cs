using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingCart.Pages.ShoppingCartPages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            return RedirectToPage("/ShoppingCartPages/Login");
        }
    }
}
