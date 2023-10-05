namespace ShoppingCart.API.Models.DTO
{
    public class OrderDTO
    {
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }
}
