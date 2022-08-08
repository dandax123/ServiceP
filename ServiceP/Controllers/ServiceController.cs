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
        public async Task<ServiceResponseDto> Get(int id)
        {

            return (await _myService.GetServiceDetails(id));

        }

        [HttpGet("{id}/bookings")]
        public async Task<IEnumerable<Booking>> GetBookingsForService(int id)
        {
            int providerId = HttpContext.GetUserIdFromToken();
            return (await _myBooking.getBookingsByService(providerId,id));

        }
        // POST api/<ServiceController>
        [HttpPost]
        public async Task<IActionResult> Post(ServiceBaseDto service)
        {

            int providerId = HttpContext.GetUserIdFromToken();
 
            await _myService.createService(providerId, service);
              
            return Ok("Created it");
        }

        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ServiceBaseDto value)
        {
            int creatorId = HttpContext.GetUserIdFromToken();
            await _myService.updateServiceDetails(creatorId, id, value);
        }

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            int creatorId = HttpContext.GetUserIdFromToken();
            await _myService.deleteService(creatorId, id);
        }
    }
}
