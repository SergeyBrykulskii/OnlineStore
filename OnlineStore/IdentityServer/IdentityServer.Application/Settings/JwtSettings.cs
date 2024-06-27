namespace IdentityServer.Application.Settings;

public class JwtSettings
{
    public const string DefaultSection = "Jwt";

    public string Issuer { get; set; }
    public string[] Audiences { get; set; }
    public string JwtKey { get; set; }
    public string Authority { get; set; }
    public int AccessTokenValidityInMinutes { get; set; }
    public int RefreshTokenValidityInDays { get; set; }
}
