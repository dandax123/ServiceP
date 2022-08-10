using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceP.Auth;
using ServiceP.Constants;
using ServiceP.DTO;
using ServiceP.Models;
using ServiceP.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Provider)]
    public class ServiceController : ControllerBase
    {
        private IService _myService;
        private IBooking _myBooking;
        public ServiceController(IService iservice, IBooking booking)
        {
            _myService = iservice;
            _myBooking = booking;
        }

        // GET: api/<ServiceController>
        [HttpGet("my_services")]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IEnumerable<ServiceDto>> GetMyServices()
        {
            int providerId = HttpContext.GetUserIdFromToken(); 
            return await _myService.getServicesByUser(providerId);
        }


        [HttpGet, AllowAnonymous]
        public async Task<IEnumerable<ServiceResponseDto>> Get()
        {
            return (await _myService.getAll());
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}"), AllowAnonymous]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<ServiceResponseDto> Get(int id)
        {

            return (await _myService.GetServiceDetails(id));

        }

        [HttpGet("{id}/bookings")]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IEnumerable<Booking>> GetBookingsForService(int id)
        {
            int providerId = HttpContext.GetUserIdFromToken();
            return (await _myBooking.getBookingsByService(providerId,id));

        }
        // POST api/<ServiceController>
        [HttpPost]
        [ProducesResponseType(typeof(ApiSuccess), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> Post(ServiceBaseDto service)
        {

            int providerId = HttpContext.GetUserIdFromToken();
 
            await _myService.createService(providerId, service);
              
            return Ok(new ApiSuccess("Successfully created the service"));
        }

        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiSuccess), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> Put(int id, [FromBody] ServiceBaseDto value)
        {
            int creatorId = HttpContext.GetUserIdFromToken();
            await _myService.updateServiceDetails(creatorId, id, value);
            
            return Ok(new ApiSuccess("Successfully updated the service"));

        }

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiSuccess), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            int creatorId = HttpContext.GetUserIdFromToken();
            await _myService.deleteService(creatorId, id);
            return Ok(new ApiSuccess("Successfully deleted the service"));

        }
    }
}
