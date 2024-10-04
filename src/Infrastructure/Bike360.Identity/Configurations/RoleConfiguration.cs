using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bike360.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "456b41e6-001b-43e2-9ac7-e4b043a9d718",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
             new IdentityRole
             {
                 Id = "d763c0cd-a5bc-4721-8421-ac4a37071495",
                 Name = "Travel Agency Employee",
                 NormalizedName = "TRAVEL AGENCY EMPLOYEE"
             },
            new IdentityRole
            {
                Id = "5bb93284-b026-4ed1-a331-5533e757a143",
                Name = "Diving School Employee",
                NormalizedName = "DIVING SCHOOL EMPLOYEE"
            });

    }
}
