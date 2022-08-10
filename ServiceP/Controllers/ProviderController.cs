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
        IProvider _myProviderService;
        public ProviderController(IProvider myProviderService)
        {
            _myProviderService = myProviderService; 
        }
        // GET: api/<ProviderController>
        [HttpGet, Authorize(Roles = Roles.Provider)]
        public async Task<UserDescribeDto> Get()
        {
            int customerId = HttpContext.GetUserIdFromToken();
            return UserDto.User2UserDescribeDTO(await _myProviderService.getById(customerId));
        }


        // POST api/<ProviderController>
        [HttpPost]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<ActionResult<string>> CustomerRegistration(ProviderRegistrationRequest request)
        {

            var token = await _myProviderService.RegisterProvider(request);

            return Ok(new LoginResponse { token = token });

        }

        // PUT api/<ProviderController>/5
        [HttpPut, Authorize(Roles = Roles.Provider)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> Put([FromBody] UserDto value)
        {
            int providerId = HttpContext.GetUserIdFromToken();
            await _myProviderService.updateProvider(providerId, value);
            return Ok(new ApiSuccess("Successfully updated Provider"));
        }


        // DELETE api/<ProviderController>/5
        [HttpDelete, Authorize(Roles = Roles.Provider)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> Delete()
        {

            int providerId = HttpContext.GetUserIdFromToken();
            await _myProviderService.deleteProvider(providerId);
            return Ok(new ApiSuccess("Successfully deleted Provider"));
        }
    }
}
