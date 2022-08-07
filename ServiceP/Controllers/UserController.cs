using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private IUser _userService;
    public UserController(IUser userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> Get()
    {
       
        return Ok(_userService.getAll());
    }

    [HttpPost]

    public async Task<ActionResult<User>> Post(User new_user)
    {
        return Ok(new_user);
    }

    [HttpGet("{user_id}")]
    public async Task<ActionResult<User>> Get (int user_id)
    {
        return Ok(new User { userId = user_id , first_name="cc", last_name="cc"});
    }

    [HttpPut]
    public async Task<ActionResult<User>> Put(User new_user)
    {
        return Ok(new_user);
    }

    [HttpDelete("{user_id}")]
    public async Task<ActionResult<User>> Delete(int user_id)
    {
        return Ok(new User { userId = user_id , first_name="cc", last_name="cc"});

    }
}
    