namespace ShoppingCart.Models.DTO
{
    public class OrderDetailDTO
    {
        public int OrderID { get; set; } 
        public int ProductID { get; set; } 
        public int Quantity { get; set; } 
        public decimal Price { get; set; } 
    }

}
