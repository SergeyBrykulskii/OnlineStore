﻿using Microsoft.AspNetCore.Identity;

namespace IdentityServer.DAL.Models;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
