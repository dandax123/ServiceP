using Microsoft.AspNetCore.Mvc;
using ServiceP.Models;

namespace ServiceP.Repository
{
    public interface IAuth
    {
        public void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool IsValidEmail(string email);
        public Task<ActionResult<String>>  RegisterCustomer(Customer t);
        public Task<ActionResult<String>> RegisterProvider(Provider t);
        public bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public string createToken(User a, string role);

        Task<String> login(string password, User a, string role);





    }
}
