using Microsoft.AspNetCore.Mvc;
using ServiceP.DTO;
using ServiceP.Models;

namespace ServiceP.Repository
{
    public interface IAuth
    {

        string createToken(User a);
        Task<string> login(LoginRequest request);





    }
}
