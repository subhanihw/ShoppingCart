using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Models.DTO;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    public interface IAPIServices
    {
        
        Task AddUser(RegistrationDto ReDto);
        Task<LoginDto> GetUsernamePassword(string username);
        
    }
}
