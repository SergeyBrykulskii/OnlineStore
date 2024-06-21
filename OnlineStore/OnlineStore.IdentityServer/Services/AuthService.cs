using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineStore.IdentityServer.Models;
using OnlineStore.IdentityServer.Models.DTOs;
using OnlineStore.IdentityServer.Models.Result;

namespace OnlineStore.IdentityServer.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public AuthService(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
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
                    ErrorMessage = "Unknown error"
                };
            }
        }
        await _userManager.AddToRolesAsync(user, userRegistrationDto.Roles);

        return new BaseResult<UserDto>()
        {
            Data = _mapper.Map<UserDto>(user)
        };
    }
}
