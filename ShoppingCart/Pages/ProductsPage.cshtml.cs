using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;
using ShoppingCart.Models.DTO;
using ShoppingCart.Services;

namespace ShoppingCart.Pages
{
    public class ProductsPageModel : PageModel
    {
        private readonly IAPIServices apiService;
        public List<Product> ProductItems { get; set; } = new List<Product>();

        public ProductsPageModel(IAPIServices apiService)
        {
            this.apiService = apiService;
        }
        public async Task OnGet()
        {
            TempData.Keep("UserID");
            ProductItems = await apiService.GetAll();
        }

        public async Task<IActionResult> OnPostAsync(int productId)
        {
            var addTocartDTO = new AddToCartDTO { UserID = Convert.ToInt32(TempData["UserID"]), ProductId = productId, Quantity = 1};
            TempData.Keep("UserID");
            await apiService.AddToCart(addTocartDTO);
            TempData["Success"] = "Added Successfully";
            return RedirectToPage("/ProductsPage"); 
        }
    }
}
