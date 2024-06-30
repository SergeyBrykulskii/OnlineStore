using IdentityServer.Application.Models.DTOs.TokenDTOs;
using IdentityServer.Application.Models.DTOs.UserDTOs;
using IdentityServer.Application.Models.Result;


namespace IdentityServer.Application.Services.Interfaces;

public interface IAuthService
{
    Task<BaseResult<UserDto>> Register(UserRegistrationDto userRegistrationDto);
    Task<BaseResult<TokenDto>> Login(UserAuthenticationDto userForAuth);
}
