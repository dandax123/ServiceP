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
    public class BookingController : ControllerBase
    {
        IBooking _myBookingService;
        public BookingController(IBooking bookingService)
        {
            _myBookingService = bookingService;
        }

        [HttpGet, Authorize(Roles = Roles.Customer)]
        public async Task<IEnumerable<Booking>> Get()
        {
            int customerId = HttpContext.GetUserIdFromToken();
            var values = await _myBookingService.getBookingsByCustomer(customerId);
            return values;
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public async Task<Booking> Get(int id)
        {
            var booking = await _myBookingService.get(id);
            return booking;
        }

        // POST api/<ServiceController>
        [HttpPost, Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> Post(BookingDto booking)
        {

            int customerId = HttpContext.GetUserIdFromToken();

            await _myBookingService.addBooking(booking.serviceId, customerId, booking.quantity);

            return Ok("Cool");
        }

        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ServiceDto value)
        {

        }

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {

        }
    }
}
