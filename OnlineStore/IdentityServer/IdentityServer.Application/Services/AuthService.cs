using AutoMapper;
using IdentityServer.Application.Enums;
using IdentityServer.Application.Models.DTOs.TokenDTOs;
using IdentityServer.Application.Models.DTOs.UserDTOs;
using IdentityServer.Application.Models.Result;
using IdentityServer.Application.Resources;
using IdentityServer.Application.Services.Interfaces;
using IdentityServer.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer.Application.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public AuthService(
        IMapper mapper,
        UserManager<User> userManager,
        ITokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<BaseResult<UserDto>> Register(UserRegistrationDto userRegistrationDto)
    {
        var user = _mapper.Map<User>(userRegistrationDto);

        var result = await _userManager.CreateAsync(user, userRegistrationDto.Password);

        if (!result.Succeeded)
        {
            if (result.Errors.Any())
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = result.Errors.First().Description
                };
            }
            else
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        await _userManager.AddToRolesAsync(user, userRegistrationDto.Roles);

        return new BaseResult<UserDto>()
        {
            Data = _mapper.Map<UserDto>(user)
        };
    }

    public async Task<BaseResult<TokenDto>> Login(UserAuthenticationDto userAuthDto)
    {
        User user = await _userManager.FindByNameAsync(userAuthDto.UserName);

        if (user == null)
        {
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };
        }

        bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, userAuthDto.Password);

        if (!isPasswordCorrect)
        {
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = ErrorMessage.IncorrectPassword,
                ErrorCode = (int)ErrorCodes.IncorrectPassword
            };
        }

        var newAccessToken = _tokenService.GenerateAccessToken(await GetClaims(user));
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);

        await _userManager.UpdateAsync(user);

        return new BaseResult<TokenDto>()
        {
            Data = new TokenDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            }
        };
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }
}
