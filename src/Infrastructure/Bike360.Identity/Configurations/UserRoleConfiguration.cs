using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bike360.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "456b41e6-001b-43e2-9ac7-e4b043a9d718",
                UserId = "fd7be93d-a089-4801-9de5-c5aa64c97962"
            },
            new IdentityUserRole<string>
            {
                RoleId = "5bb93284-b026-4ed1-a331-5533e757a143",
                UserId = "90ebb8bd-27fc-4c78-968f-9d93d267a502"
            });
    }
}
