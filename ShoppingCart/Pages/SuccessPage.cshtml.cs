using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;
using ShoppingCart.Models.DTO;
using ShoppingCart.Services;

namespace ShoppingCart.Pages
{
    public class SuccessPageModel : PageModel
    {
        private readonly IAPIServices apiService;

        public SuccessPageModel(IAPIServices apiService)
        {
            this.apiService = apiService;
        }
        public async Task OnGetAsync()
        {
            var userID = Convert.ToInt32(TempData["UserID"]);
            TempData.Keep("UserID");
            var cartItems = await apiService.GetCartItems(userID);
            var Total = await apiService.GetTotalPriceCart(userID);
            var order = new OrderDTO
            {
                UserID = userID,
                OrderDate = DateTime.Now,
                Total = Total,
            };
            int orderID = await apiService.InsertOrder(order);
            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetailDTO
                {
                    OrderID = orderID,
                    ProductID = cartItem.ProductID,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Price,
                };
                await apiService.DeleteAllCartItems(userID);
                await apiService.InsertOrderDetails(orderDetail);
            }
        }
    }
}
