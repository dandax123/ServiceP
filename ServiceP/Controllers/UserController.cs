using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceP.Constants;
using ServiceP.DTO;
using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Controllers;

[Route("api/Admin")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class UserController : ControllerBase
{

    private IUser _userService;
    private IBooking _myBookingService;
    public UserController(IUser userService, IBooking myBookingService)
    {
        _userService = userService;
        _myBookingService = myBookingService;
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


    [HttpGet("bookings")]
    public async Task<IEnumerable<BookingDisplayDto>> GetBookings()
    {

        return (await _myBookingService.getAll());
    }





}
