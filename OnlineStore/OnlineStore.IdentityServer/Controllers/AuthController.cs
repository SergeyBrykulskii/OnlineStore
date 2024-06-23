﻿using Microsoft.AspNetCore.Mvc;
using OnlineStore.IdentityServer.Models.DTOs;
using OnlineStore.IdentityServer.Models.Result;
using OnlineStore.IdentityServer.Services;

namespace OnlineStore.IdentityServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<BaseResult<UserDto>>> RegisterUser(
        [FromBody] UserRegistrationDto userRegistrationDto)
    {
        var response = await _authService.Register(userRegistrationDto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto user)
    {
        if (!await _authService.ValidateUser(user))
        {
            //_logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
            return Unauthorized();
        }
        return Ok(new { Token = await _authService.CreateToken() });
    }
}