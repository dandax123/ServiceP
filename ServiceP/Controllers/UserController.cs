using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceP.DTO;
using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class UserController : ControllerBase
{

    private IUser _userService;
    public UserController(IUser userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<User>> Get()
    {

        return (await _userService.getAll());
    }

    [HttpGet("customers")]
    public async Task<List<User>> GetCustomers()
    {

        return (await _userService.getAll());
    }

    [HttpGet("providers")]
    public async Task<List<User>> GetProviders()
    {

        return (await _userService.getAll());
    }


    

}
    