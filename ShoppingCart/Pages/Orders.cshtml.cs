using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Models;
using ShoppingCart.Services;

namespace ShoppingCart.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly IAPIServices apiServices;
        public List<Order> Orders { get; set; } = new List<Order>();

        public OrdersModel(IAPIServices apiServices)
        {
            this.apiServices = apiServices;
        }
        public async Task OnGetAsync()
        {
            var userID = Convert.ToInt32(TempData["UserID"]);
            TempData.Keep("UserID");
            Orders = await apiServices.GetOrderByUserID(userID);          
        }
    }
}
