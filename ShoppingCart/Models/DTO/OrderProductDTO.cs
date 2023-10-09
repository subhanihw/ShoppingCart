namespace ShoppingCart.Models.DTO
{
    public class OrderProductDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
