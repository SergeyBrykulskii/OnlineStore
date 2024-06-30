using IdentityServer.Application.Models.DTOs.TokenDTOs;
using IdentityServer.Application.Models.Result;
using IdentityServer.Application.Services.Interfaces;
using IdentityServer.Application.Settings;
using IdentityServer.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityServer.Application.Services;

public class TokenService : ITokenService
{
    private readonly string _jwtKey;
    private readonly string _issuer;
    private readonly string[] _audiences;
    private readonly int _accessTokenValidityInMinutes;
    private readonly UserManager<User> _userManager;

    public TokenService(
        IOptions<JwtSettings> jwtSettings,
        UserManager<User> userManager)
    {
        _jwtKey = jwtSettings.Value.JwtKey;
        _issuer = jwtSettings.Value.Issuer;
        _audiences = jwtSettings.Value.Audiences;
        _accessTokenValidityInMinutes = jwtSettings.Value.AccessTokenValidityInMinutes;
        _userManager = userManager;
    }

    public string GenerateAccessToken(List<Claim> claims)
    {
        var signingCredentials = GetSigningCredentials();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    public string GenerateRefreshToken()
    {
        var randomNumbers = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();

        randomNumberGenerator.GetBytes(randomNumbers);

        return Convert.ToBase64String(randomNumbers);
    }

    public async Task<BaseResult<TokenDto>> RefreshToken(TokenDto tokenDto)
    {
        var accessToken = tokenDto.AccessToken;
        var refreshToken = tokenDto.RefreshToken;

        var claimsPrincipal = GetPrincipalFromExpiredToken(accessToken);

        var userName = claimsPrincipal.Identity?.Name;

        var user = await _userManager.FindByNameAsync(userName);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = "invalid token",
                ErrorCode = 666
            };
        }

        var newAccessToken = GenerateAccessToken(claimsPrincipal.Claims.ToList());
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);

        await _userManager.UpdateAsync(user);

        return new BaseResult<TokenDto>()
        {
            Data = new TokenDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            }
        };
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
            ValidAudiences = _audiences,
            ValidIssuer = _issuer
        };

        var claimsPrincipal = new JwtSecurityTokenHandler()
            .ValidateToken(accessToken, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("invalid token");
        }

        return claimsPrincipal;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtKey);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var validAudiences = _audiences;

        foreach (var audience in validAudiences)
        {
            claims.Add(new Claim("aud", audience));
        }

        var tokenOptions = new JwtSecurityToken
        (
            issuer: _issuer,
            audience: string.Empty,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_accessTokenValidityInMinutes),
            signingCredentials: signingCredentials
        );

        return tokenOptions;
    }
}
