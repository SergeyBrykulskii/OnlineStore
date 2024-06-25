using IdentityServer.Application.Models.DTOs;
using IdentityServer.Application.Models.Result;
using IdentityServer.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register"), Authorize]
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
        var response = await _authService.ValidateUser(user);
        if (response.IsSuccess)
        {
            return Ok(new { Token = await _authService.CreateToken() });
        }

        return BadRequest(response);
    }
}
