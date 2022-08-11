using Microsoft.IdentityModel.Tokens;
using ServiceP.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ServiceP.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceP.DTO;
using ServiceP.Constants;

namespace ServiceP.Services
{
    public class AuthService : IAuth
    {

        IConfiguration _conf;


        public AuthService(IConfiguration _t)
        {
            _conf = _t;
        }

        

 

        public static bool IsValidEmail(string email)
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

        public static void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public  string createToken(User a)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, a.email),
                new Claim (ClaimTypes.Role, a.role),
                new Claim(ClaimTypes.Name, a.userId.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_conf.GetSection("JwtSettings:secret").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var jwt = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(10), signingCredentials: cred);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }

  
    }
}
