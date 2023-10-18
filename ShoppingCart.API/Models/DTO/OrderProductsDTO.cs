namespace ShoppingCart.API.Models.DTO
{
    public class OrderProductsDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
