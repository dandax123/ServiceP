using Microsoft.IdentityModel.Tokens;
using ServiceP.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ServiceP.Models;
using Microsoft.AspNetCore.Mvc;

namespace ServiceP.Services.main
{
    public class AuthService: IAuth
    {

        IConfiguration _conf;
        IUser _myUserService;

        public async Task<ActionResult<String>> RegisterCustomer (Customer t)
        {
            if (!IsValidEmail(t.email))
            {
                throw new Exception("Invalid email");
            }
            await _myUserService.createCustomer(t);

            return createToken(t, "Customer");
        }

        public async Task<ActionResult<String>> RegisterProvider(Provider t)

        {
            if (!IsValidEmail(t.email))
            {
                throw new Exception("Invalid email");
            }
            await _myUserService.createProvider(t);

            return createToken(t, "Provider");
        }
        public AuthService(IConfiguration _t, IUser userService)
        {
            _conf = _t;
            _myUserService = userService;
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

        public async Task<String> login(string password, User a, string role)
        {
            var correct_password = verifyPasswordHash(password, a.password_hash, a.password_salt);
            if(!correct_password)
            {
                throw new Exception("Wrong password");
            }
            return createToken(a, role);
        }
    }
}
