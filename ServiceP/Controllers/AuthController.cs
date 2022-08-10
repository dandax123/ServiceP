using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceP.DTO;
using ServiceP.Models;
using ServiceP.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private  IAuth _myAuthService;
        

        public Customer customer;
       


        public AuthController(IAuth authService, IProvider myPService, ICustomer myCService)
        {
            _myAuthService = authService;

        }
        





        [HttpPost("login"), AllowAnonymous]

        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> Login (LoginRequest request)
        {
            var token = await _myAuthService.login(request);
            return Ok(new LoginResponse { token = token });
        }

        
    }
}
