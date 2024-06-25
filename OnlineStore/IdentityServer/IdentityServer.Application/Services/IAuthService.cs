using IdentityServer.Application.Models.DTOs;
using IdentityServer.Application.Models.Result;


namespace IdentityServer.Application.Services;

public interface IAuthService
{
    Task<BaseResult<UserDto>> Register(UserRegistrationDto userRegistrationDto);
    Task<BaseResult> ValidateUser(UserAuthenticationDto userForAuth);
    Task<string> CreateToken();
}
