

using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models.DTO
{
    public class RegistrationDto
    {

        
        public string Username { get; set; }

        
        public string Password { get; set; }

        
        public string Gender { get; set; }

        
        public string PhoneNumber { get; set; }

        
        public string State { get; set; }
        public string Exceptionname { get; set; } = string.Empty;
    }
}
