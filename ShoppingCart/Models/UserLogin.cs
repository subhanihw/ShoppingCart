//using System.ComponentModel.DataAnnotations;

//namespace ShoppingCart.Models
//{
//    public class UserLogin
//    {
//        [Required(ErrorMessage = "Username is required.")]
//        public string Username { get; set; }


//        [Required(ErrorMessage = "Password is required.")]
//        [DataType(DataType.Password)]
//        public string Password { get; set; }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    //[ValidateAntiForgeryToken]

    public class UserLogin
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must not exceed 50 characters.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        //[RegularExpression(@"^(?=.*\d.*\d.*\d)[A-Za-z\d]{6,100}$", ErrorMessage = "Password must be at least 6 characters long and contain at least 3 numbers.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

