using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private IProvider _myProviderService;
        private ICustomer _myCustomerService;

        public Customer customer;
       


        public AuthController(IAuth authService, IProvider myPService, ICustomer myCService)
        {
            _myAuthService = authService;
            _myProviderService = myPService;
            _myCustomerService = myCService;
        }
        
        // POST api/<AuthController>
        [HttpPost("register/provider")]
        public async Task<ActionResult<Auth.LoginResponse>> ProviderRegistration(Auth.ProviderRegistrationRequest request)
        {
            //validation and others
            _myAuthService.createPasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            
            Provider provider = new Provider {
                password_hash = passwordHash,
                password_salt = passwordSalt,
                email = request.email,
                first_name = request.first_name,
                services = new List<Service>(),
                brand_name = request.brand_name,
                last_name = request.last_name
              };

            var token =  await _myAuthService.RegisterProvider(provider);
            var response = new Auth.LoginResponse
            {
                token = token.Value
            };

            return Ok(response);

        }


        [HttpPost("register/customer")]
        public async Task<ActionResult<String>> CustomerRegistration(Auth.BaseRegistrationRequest request)
        {
            //validation and others
            _myAuthService.createPasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);

            Customer customer = new Customer
            {
                password_hash = passwordHash,
                password_salt = passwordSalt,
                email = request.email,
                first_name = request.first_name,
                bookings = new List<Booking>(), 
                last_name = request.last_name,
            };

            var token = await _myAuthService.RegisterCustomer(customer);

            return Ok(new Auth.LoginResponse { token = token.Value});

        }


        [HttpPost("login/customer"), AllowAnonymous]
        public async Task<ActionResult<Auth.LoginResponse>> CustomerLogin (Auth.LoginRequest request)
        {

            Customer? a = (Customer )await _myCustomerService.getByEmail(request.email);
            if (a == null)
            {
                return BadRequest("User name or password is incorrect");
            }
            var token = await _myAuthService.login(request.password, a, "Customer");
            return Ok(new Auth.LoginResponse { token = token });
        }

        [HttpPost("login/provider"), AllowAnonymous]
        public async Task<ActionResult<Auth.LoginResponse>> ProviderLogin(Auth.LoginRequest request)
        {

            Provider? a = await _myProviderService.getByEmail(request.email);
            if (a == null)
            {
                return BadRequest("User name or password is incorrect");
            }
            var token = await _myAuthService.login(request.password, a, "Provider");
            return Ok(new Auth.LoginResponse { token = token});
        }


        
    }
}
