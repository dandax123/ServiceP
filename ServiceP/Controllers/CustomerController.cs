using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceP.Auth;
using ServiceP.Constants;
using ServiceP.DTO;
using ServiceP.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        IUser _myCustomerService;
        public CustomerController(IUser myCustomerService)
        {
            _myCustomerService = myCustomerService; 
        }
        // GET: api/<CustomerController>


        [HttpGet, Authorize(Roles = Roles.Customer)]
        public async Task<UserDescribeDto> Get()
        {
            int customerId = HttpContext.GetUserIdFromToken();
            return UserDto.User2UserDescribeDTO(await _myCustomerService.GetById(customerId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> CustomerRegistration(BaseRegistrationRequest request)
        {
           
            var token = await _myCustomerService.RegisterCustomer(request);

            return Ok(new LoginResponse { token = token });

        }


        [HttpPut, Authorize(Roles = Roles.Customer)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> Put([FromBody] UserDto value)
        {
            int customerId = HttpContext.GetUserIdFromToken();
            await _myCustomerService.updateUser(customerId, value);
            return Ok(new ApiSuccess("Successfully updated customer"));
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete, Authorize(Roles = Roles.Customer)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> Delete()
        {
            int customerId = HttpContext.GetUserIdFromToken();
            await _myCustomerService.deleteUser(customerId);
            return Ok(new ApiSuccess("Successfully deleted customer"));
        }
    }
}
