using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models.DTO;
using ShoppingCart.Services;

namespace ShoppingCart.Pages
{
    public class OrderDetailsModel : PageModel
    {
        private readonly IAPIServices apiServices;
        public List<OrderProductDTO> Products { get; set; } = new List<OrderProductDTO>();
        public int OrderID { get; set; }

        public OrderDetailsModel(IAPIServices apiServices)
        {
            this.apiServices = apiServices;
        }
        public async Task OnGetAsync()
        {
            if (int.TryParse(HttpContext.Request.Query["OrderID"], out int orderId))
            {
                OrderID = orderId;
            }
            var userID = Convert.ToInt32(TempData["UserID"]);
            TempData.Keep("UserID");
            Products = await apiServices.GetProductDetails(userID, OrderID);
        }
    }
}
