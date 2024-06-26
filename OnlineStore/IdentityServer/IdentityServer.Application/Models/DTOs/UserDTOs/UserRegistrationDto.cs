﻿namespace IdentityServer.Application.Models.DTOs.UserDTOs;

public class UserRegistrationDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<string> Roles { get; set; }
}
