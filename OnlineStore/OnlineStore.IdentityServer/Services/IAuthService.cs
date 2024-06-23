using OnlineStore.IdentityServer.Models.DTOs;
using OnlineStore.IdentityServer.Models.Result;


namespace OnlineStore.IdentityServer.Services;

public interface IAuthService
{
    Task<BaseResult<UserDto>> Register(UserRegistrationDto userRegistrationDto);
    Task<bool> ValidateUser(UserAuthenticationDto userForAuth);
    Task<string> CreateToken();
}
