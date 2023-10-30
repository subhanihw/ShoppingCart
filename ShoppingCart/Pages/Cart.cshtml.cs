using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;
using ShoppingCart.Models.DTO;
using ShoppingCart.Services;

namespace ShoppingCart.Pages
{
    public class CartModel : PageModel
    {
        private readonly IAPIServices apiService;
        public List<CartItemsDTO> CartItems { get; set; } = new List<CartItemsDTO>();
        public decimal Total { get; set; }

        public CartModel(IAPIServices apiService)
        {
            this.apiService = apiService;
        }
        public async Task OnGetAsync()
        {
            var userID = Convert.ToInt32(TempData["UserID"]);
            TempData.Keep("UserID");
            CartItems = await apiService.GetCartItems(userID);
            Total = await apiService.GetTotalPriceCart(userID);      
        }
        public async Task<IActionResult> OnPostDelete(int productID)
        {
            var userID = Convert.ToInt32(TempData["UserID"]);
            TempData.Keep("UserID");
            await apiService.DeleteCartItem(userID, productID);
            Total = await apiService.GetTotalPriceCart(userID);
            return RedirectToPage("/Cart");
        }

        public async Task<IActionResult> OnPostUpdateQuantity()
        {
            var userID = Convert.ToInt32(TempData["UserID"]);
            TempData.Keep("UserID");
            int productID = int.Parse(Request.Form["productID"]);
            int quantity = int.Parse(Request.Form["quantity"]);
            await apiService.AddToCart(new AddToCartDTO
            {
                ProductId = productID,
                Quantity = quantity,
                UserID = userID
            });
            return RedirectToPage("/Cart");
        }
    }
}
