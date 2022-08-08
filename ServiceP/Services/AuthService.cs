using Microsoft.IdentityModel.Tokens;
using ServiceP.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ServiceP.Models;
using Microsoft.AspNetCore.Mvc;

namespace ServiceP.Services
{
    public class AuthService : IAuth
    {

        IConfiguration _conf;


        ICustomer _myCustomer;
        IProvider _myProvider;
        public AuthService(IConfiguration _t, IUser userService, ICustomer customer, IProvider provider)
        {
            _conf = _t;

            _myCustomer = customer;
            _myProvider = provider;
        }

        public async Task<ActionResult<string>> RegisterCustomer(Customer t)
        {
            if (!IsValidEmail(t.email))
            {
                throw new AppException("Invalid email address given. Provide a valid email address");
            }
            await _myCustomer.createCustomer(t);

            return createToken(t, "Customer");
        }

        public async Task<ActionResult<string>> RegisterProvider(Provider t)

        {
            if (!IsValidEmail(t.email))
            {
                throw new AppException("Invalid email address given. Provide a valid email address");
            }
            await _myProvider.createProvider(t);

            return createToken(t, "Provider");
        }

        public bool IsValidEmail(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                 @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex regex = new Regex(strRegex);
            if (regex.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string createToken(User a, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, a.email),
                new Claim (ClaimTypes.Role, role ),
                new Claim(ClaimTypes.Name, a.userId.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_conf.GetSection("JwtSettings:secret").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var jwt = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(10), signingCredentials: cred);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }

        public async Task<string> login(string password, User a, string role)
        {
            var correct_password = verifyPasswordHash(password, a.password_hash, a.password_salt);
            if (!correct_password)
            {
                throw new AppException("Incorrect username or password!");
            }
            return createToken(a, role);
        }
    }
}
