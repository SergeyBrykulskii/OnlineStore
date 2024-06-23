using System.ComponentModel.DataAnnotations;

namespace OnlineStore.IdentityServer.Models.DTOs;

public class UserAuthenticationDto
{
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Password name is required")]
    public string Password { get; set; }
}
