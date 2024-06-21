using System.ComponentModel.DataAnnotations;

namespace OnlineStore.IdentityServer.Models.DTOs;

public class UserRegistrationDto
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<string> Roles { get; set; }
}
