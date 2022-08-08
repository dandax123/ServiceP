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
    public class ServiceController : ControllerBase
    {
        private IService _myService;
        public ServiceController(IService iservice)
        {
            _myService = iservice;
        }

        // GET: api/<ServiceController>
        [HttpGet]
        public async Task<IEnumerable<Service>> Get()
        {
            var values = await _myService.getAll();
            return values;
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public async Task<Service> Get(int id)
        {
            var service = await _myService.GetService(id);
            return service;
        }

        // POST api/<ServiceController>
        [HttpPost, Authorize(Roles = Roles.Provider)]
        public async Task<IActionResult> Post(ServiceDto service)
        {

            int providerId = HttpContext.GetUserIdFromToken();
 
            await _myService.createService(providerId, service);
              
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
