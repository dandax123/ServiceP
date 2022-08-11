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
    public class ProviderController : ControllerBase
    {
        IUser _myProviderService;
        IBooking _myBookingService;
        public ProviderController(IUser myProviderService, IBooking myBookingService)
        {
            _myProviderService = myProviderService;
            _myBookingService = myBookingService;
        }
        // GET: api/<ProviderController>
        [HttpGet, Authorize(Roles = Roles.Provider)]
        public async Task<UserDescribeDto> Get()
        {
            int customerId = HttpContext.GetUserIdFromToken();
            return UserDto.User2UserDescribeDTO(await _myProviderService.GetById(customerId));
        }


        // POST api/<ProviderController>
        [HttpPost]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> CustomerRegistration(ProviderRegistrationRequest request)
        {

            var token = await _myProviderService.RegisterProvider(request);

            return Ok(new LoginResponse { token = token });

        }

        // PUT api/<ProviderController>/5
        [HttpPut, Authorize(Roles = Roles.Provider)]
        [ProducesResponseType(typeof(ApiSuccess), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> Put([FromBody] UserDto value)
        {
            int providerId = HttpContext.GetUserIdFromToken();
            await _myProviderService.updateUser(providerId, value);
            return Ok(new ApiSuccess("Successfully updated Provider"));
        }


        // DELETE api/<ProviderController>/5
        [HttpDelete, Authorize(Roles = Roles.Provider)]
        [ProducesResponseType(typeof(ApiSuccess), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> Delete()
        {

            int providerId = HttpContext.GetUserIdFromToken();
            await _myProviderService.deleteUser(providerId);
            return Ok(new ApiSuccess("Successfully deleted Provider"));
        }

        [HttpGet("bookings"), Authorize(Roles = Roles.Provider)]
        [ProducesResponseType(typeof(IEnumerable<BookingDisplayDto>), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IEnumerable<BookingDisplayDto>> GetMyBookings()
        {

            int providerId = HttpContext.GetUserIdFromToken();
            return (await _myBookingService.getBookingsByProvider(providerId));
     
        }
    }
}
