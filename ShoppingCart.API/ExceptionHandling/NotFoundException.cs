namespace ShoppingCart.API.ExceptionHandling
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
    }

}
