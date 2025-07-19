using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ORAA.Core;
using ORAA.Data;
using ORAA.DTO;
using ORAA.Enums;
using ORAA.Models;
using ORAA.Request;
using ORAA.Services.Interfaces;
using ORAA.SMTP;
using ORAA.Validations;

namespace ORAA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IJWTService _jwtService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(DataContext context, IJWTService jwtService, IMapper mapper, IUserService userService)
    {
        _context = context;
        _jwtService = jwtService;
        _mapper = mapper;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(AddUser request)
    {
        var User = await _userService.RegisterUser(request);
        return Ok(User);
    }

    [HttpPost("verify-email/{email}/{code}")]
    public async Task<ActionResult> Verify(string email, string code)
    {
        var VerifiedUser = await _userService.Verify(email, code);
        return Ok(VerifiedUser);
    }

    [HttpGet("get-profile")]
    //[Authorize]
    public ActionResult GetProfile(int id)
    {
        var getProfile = _userService.GetProfile(id);
        return Ok(getProfile);
    }

    [HttpPost("get-reset-code")]
    public ActionResult GetResetCode(string userEmail)
    {
        var getResetCode = _userService.GetResetCode(userEmail);
        return Ok(getResetCode);
    }

    [HttpPut("reset-password")]
    public async Task<ActionResult> ResetPassword(string email, string code, string newPassword)
    {
        var resetPassword = await _userService.ResetPassword(email, code, newPassword);
        return Ok(resetPassword);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(string email, string password)
    {
        var userlogin = await _userService.Login(email, password);
        return Ok(userlogin);
    }

    [HttpPut("update-user")]
    public async Task<ActionResult> UpdateUser(int id, string changeParamert, string changeTo)
    {
        var user = await _userService.UpdateUser(id, changeParamert, changeTo);

        return Ok(user);
    }

    [HttpDelete("delete-user")]
    public ActionResult DeleteUser(int id)
    {
        var user = _userService.DeleteUser(id);

        return Ok(user);
    }
}