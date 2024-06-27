using IdentityServer.Application.Models.DTOs.TokenDTOs;
using IdentityServer.Application.Models.DTOs.UserDTOs;
using IdentityServer.Application.Models.Result;
using IdentityServer.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(
        IAuthService authService,
        ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
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
    public async Task<ActionResult<BaseResult<TokenDto>>> Authenticate(
        [FromBody] UserAuthenticationDto user)
    {
        var response = await _authService.Login(user);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken(
        [FromBody] TokenDto tokenDto)
    {
        var response = await _tokenService.RefreshToken(tokenDto);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}
