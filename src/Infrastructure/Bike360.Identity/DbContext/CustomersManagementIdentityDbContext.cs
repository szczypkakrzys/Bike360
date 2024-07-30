using Bike360.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bike360.Identity.DbContext;

public class CustomersManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public CustomersManagementIdentityDbContext(DbContextOptions<CustomersManagementIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(CustomersManagementIdentityDbContext).Assembly);
    }
}
