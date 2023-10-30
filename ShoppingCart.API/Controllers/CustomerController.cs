using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.ExceptionHandling;
using ShoppingCart.API.Models.DTO;
using ShoppingCart.API.Repositories;

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
            try
            {
                var customers = await repository.GetCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCustomerByID([FromRoute] int id)
        {
            try
            {
                var customer = await repository.GetCustomerByIdAsync(id);
                return Ok(customer);
            }catch (NotFoundException ex){
                return NotFound(ex.Message);
            } 
            catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            } 
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddCustomer(CustomerDTO Customer)
        {
            try
            {
                var NewCustomer = await repository.AddCustomer(Customer);
                return Ok(NewCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var Customer = await repository.DeleteCustomerAsync(id);
                return Ok(Customer);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] CustomerDTO Customer)
        {
            try
            {
                var Cust = await repository.UpdateCustomerAsync(id, Customer);
                return Ok(Cust);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
        [Route("ValidateUser")]
        public async Task<IActionResult> ValidateUser([FromQuery] string username, [FromQuery] string password)
        {
           try
            {
                var isExists = await repository.ValidateUserNamePassword(username, password);
                var response = new AuthenticationResponse
                {
                    IsAuthenticated = isExists
                };

                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
