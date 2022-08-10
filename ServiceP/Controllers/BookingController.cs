using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceP.Auth;
using ServiceP.Constants;
using ServiceP.DTO;
using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Customer)]
    public class BookingController : ControllerBase
    {
        IBooking _myBookingService;
        public BookingController(IBooking bookingService)
        {
            _myBookingService = bookingService;
        }

        [HttpGet]
        public async Task<IEnumerable<Booking>> Get()
        {
            int customerId = HttpContext.GetUserIdFromToken();
            var values = await _myBookingService.getBookingsByCustomer(customerId);
            return values;
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<Booking> Get(int id)
        {

            int customerId = HttpContext.GetUserIdFromToken();
            var booking = await _myBookingService.getBookingDetails(customerId, id);
            return booking;
        }

        // POST api/<ServiceController>
        [HttpPost]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiSuccess), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> Post(BookingDto booking)
        {

            int customerId = HttpContext.GetUserIdFromToken();

            await _myBookingService.addBooking(booking.serviceId, customerId, booking.quantity);

            return Ok(new ApiSuccess ( "Successfully created the booking"));
        }

        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiSuccess), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> Put(int id, [FromBody] BookingUpdateDto request)
        {
            int customerId = HttpContext.GetUserIdFromToken();
            await _myBookingService.updateBooking(customerId, id, request.quantity);
            return Ok(new ApiSuccess("Successfully updated the booking"));
        }

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiSuccess), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            int customerId = HttpContext.GetUserIdFromToken();
            await _myBookingService.deleteBooking(customerId, id);
            return Ok(new ApiSuccess("Successfully deleted the booking"));
        }
    }
}
