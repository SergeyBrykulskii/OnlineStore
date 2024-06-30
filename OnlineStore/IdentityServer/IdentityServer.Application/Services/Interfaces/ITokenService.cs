using IdentityServer.Application.Models.DTOs.TokenDTOs;
using IdentityServer.Application.Models.Result;
using System.Security.Claims;

namespace IdentityServer.Application.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(List<Claim> claims);
    string GenerateRefreshToken();
    Task<BaseResult<TokenDto>> RefreshToken(TokenDto tokenDto);
}
