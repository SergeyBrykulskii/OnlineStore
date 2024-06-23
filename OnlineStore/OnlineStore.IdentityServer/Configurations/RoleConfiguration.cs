﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineStore.IdentityServer.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
        new IdentityRole
        {
            Name = "DefaultUser",
            NormalizedName = "DEFAULTUSER"
        },
        new IdentityRole
        {
            Name = "Admin",
            NormalizedName = "ADMIN"
        }
        );
    }
}