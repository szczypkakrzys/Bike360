using Bike360.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bike360.Persistence.DatabaseContext;

public class Bike360DatabaseContext : DbContext
{
    public Bike360DatabaseContext(
        DbContextOptions<Bike360DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Bike> Bikes { get; set; }

    //public DbSet<DivingCourse> DivingCourses { get; set; }
    //public DbSet<CustomersDivingCoursesRelations> CustomersDivingCoursesRelations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Bike360DatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);

        //TODO - adjust for new business

        modelBuilder.Entity<Address>()
            .HasOne(e => e.Customer)
            .WithOne(e => e.Address)
            .HasForeignKey<Customer>(e => e.AddressId)
            .IsRequired();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.TimeLastModifiedInUtc = DateTime.UtcNow;
            //entry.Entity.LastModifiedBy = _userService.UserId;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.TimeCreatedInUtc = DateTime.UtcNow;
                //entry.Entity.CreatedBy = _userService.UserId;
            }
            else
            {
                entry.Property(nameof(BaseEntity.TimeCreatedInUtc)).IsModified = false;
                //entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
