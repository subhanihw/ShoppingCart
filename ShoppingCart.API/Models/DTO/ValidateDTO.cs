using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.Models.DTO
{
    public class ValidateDTO
    {
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
    }
}
