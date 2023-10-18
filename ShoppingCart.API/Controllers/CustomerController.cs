
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Models.DTO;
using ShoppingCart.API.Repositories;
using System.Data.SqlClient;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository repository;

        public CustomerController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await repository.GetCustomersAsync();
            return Ok(customers);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCustomerByID([FromRoute] int id)
        {
            var customer = await repository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound($"Customer with ID = {id} not found");
            }
            return Ok(customer);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddCustomer(CustomerDTO Customer)
        {
            try
            {
                var NewCustomer = await repository.AddCustomer(Customer);
                if (string.IsNullOrEmpty(NewCustomer.Exceptionname))
                {

                    return Ok(NewCustomer);
                }
                return BadRequest(NewCustomer.Exceptionname);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var Customer = await repository.DeleteCustomerAsync(id);
            if (Customer != null)
            {
                return Ok(Customer);
            }
            return NotFound($"Customer with ID = {id} not found");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] CustomerDTO Customer)
        {
            var Cust = await repository.UpdateCustomerAsync(id, Customer);

            if (Cust != null)
            {
                return Ok(Cust);
            }
            return NotFound($"Customer with ID = {id} not found");
        }

        [HttpGet]
        [Route("Validate")]
        public async Task<IActionResult> GetPasswordByUserName([FromQuery] string username)
        {
            var validateDTO = await repository.GetPasswordByUserNameAsync(username);

            if (validateDTO.password != null)
            {
                return Ok(validateDTO);
            }
            return BadRequest("Invalid Credentials");
        }
        [HttpGet]
        [Route("Login")]
        public async Task<bool> GetPasswordAndUserName([FromQuery] string username, string password)
        {
            var validateDTO = await repository.GetPasswordAndUsernameAsync(username, password);

            if (validateDTO)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

