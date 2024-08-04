using Bike360.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bike360.Persistence.DatabaseContext;

public class WorkshopDatabaseContext : DbContext
{
    public WorkshopDatabaseContext(
        DbContextOptions<WorkshopDatabaseContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }

    //public DbSet<DivingCourse> DivingCourses { get; set; }
    //public DbSet<CustomersDivingCoursesRelations> CustomersDivingCoursesRelations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkshopDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Address>()
            .HasOne(e => e.Customer)
            .WithOne(e => e.Address)
            .HasForeignKey<Customer>(e => e.AddressId)
            .IsRequired();

        //modelBuilder.Entity<DivingCourse>()
        //    .HasMany(e => e.Participants)
        //    .WithMany(e => e.DivingCourses)
        //    .UsingEntity<CustomersDivingCoursesRelations>(
        //        l => l.HasOne(e => e.Customer)
        //                .WithMany(e => e.DivingCoursesRelations)
        //                .HasForeignKey(e => e.CustomerId),
        //        r => r.HasOne(e => e.DivingCourse)
        //                .WithMany(e => e.DivingCourseRelations)
        //                .HasForeignKey(e => e.DivingCourseId));
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
