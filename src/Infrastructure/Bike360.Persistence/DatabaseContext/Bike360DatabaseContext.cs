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
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ReservationBike> ReservationBikes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Bike360DatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Address>()
            .HasOne(e => e.Customer)
            .WithOne(e => e.Address)
            .HasForeignKey<Customer>(e => e.AddressId)
            .IsRequired();

        modelBuilder.Entity<Reservation>()
            .HasOne(e => e.Customer)
            .WithMany(e => e.Reservations)
            .HasForeignKey(e => e.CustomerId)
            .IsRequired();

        modelBuilder.Entity<Reservation>()
            .HasMany(e => e.Bikes)
            .WithMany(e => e.Reservations)
            .UsingEntity<ReservationBike>(
                l => l.HasOne<Bike>()
                        .WithMany(e => e.ReservationBikes)
                        .HasForeignKey(e => e.BikeId),
                r => r.HasOne<Reservation>()
                        .WithMany(e => e.ReservationBikes)
                        .HasForeignKey(e => e.ReservationId));
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
