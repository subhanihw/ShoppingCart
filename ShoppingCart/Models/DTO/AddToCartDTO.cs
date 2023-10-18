namespace ShoppingCart.Models.DTO
{
    public class AddToCartDTO
    {
        public int UserID { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
