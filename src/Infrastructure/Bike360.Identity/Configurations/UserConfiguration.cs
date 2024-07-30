using Bike360.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bike360.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(
            new ApplicationUser
            {
                Id = "fd7be93d-a089-4801-9de5-c5aa64c97962",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "P@$$w0rd"),
                EmailConfirmed = true
            },
            new ApplicationUser
            {
                Id = "90ebb8bd-27fc-4c78-968f-9d93d267a502",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                FirstName = "System",
                LastName = "User",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "P@$$w0rd"),
                EmailConfirmed = true
            });
    }
}
