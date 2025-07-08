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

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] AddUser request)
    {
        
        var response = await _userService.RegisterUser(request);
       
        return Ok(response);
    }

    [HttpGet("")]

    [HttpPost("Login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var response = await _userService.Login(email , password);
        
            var userToken = response.Data;
            _jwtService.WriteAuthTokenAsHttpOnlyCookie("auth_token", userToken.Token, DateTime.Now.AddMinutes(30));
        
        return Ok(response);
    }
}