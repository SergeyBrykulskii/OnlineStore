using IdentityServer.Models.DTOs;
using IdentityServer.Models.Result;


namespace IdentityServer.Services;

public interface IAuthService
{
    Task<BaseResult<UserDto>> Register(UserRegistrationDto userRegistrationDto);
    Task<BaseResult> ValidateUser(UserAuthenticationDto userForAuth);
    Task<string> CreateToken();
}
