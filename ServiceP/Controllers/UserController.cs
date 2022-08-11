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
    public async Task<List<UserDescribeDto>> Get()
    {

        return (await _userService.getAll());
    }

    
    [HttpGet("providers")]
    public async Task<List<UserDto>> GetProviders()
    {

        return (await _userService.getAllProviders());
    }


    

}
    