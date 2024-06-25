using AutoMapper;
using IdentityServer.Models;
using IdentityServer.Models.DTOs;
using IdentityServer.Models.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServer.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    private User _user;

    public AuthService(
        IMapper mapper,
        UserManager<User> userManager,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
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

    public async Task<BaseResult> ValidateUser(UserAuthenticationDto userAuthDto)
    {
        _user = await _userManager.FindByNameAsync(userAuthDto.UserName);

        if (_user == null)
        {
            return new BaseResult()
            {
                ErrorMessage = "User not found",

            };
        }
        bool isPasswordCorrect = await _userManager.CheckPasswordAsync(_user, userAuthDto.Password);
        if (!isPasswordCorrect)
        {
            return new BaseResult()
            {
                ErrorMessage = "Incorrect password"
            };
        }

        return new BaseResult();
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:JwtKey"));
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, _user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(_user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var validAudiences = jwtSettings.GetSection("ValidAudience").Get<string[]>();

        foreach (var audience in validAudiences)
        {
            claims.Add(new Claim("aud", audience));
        }

        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings.GetSection("ValidIssuer").Value,
            audience: string.Empty,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("Expires").Value)),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}
