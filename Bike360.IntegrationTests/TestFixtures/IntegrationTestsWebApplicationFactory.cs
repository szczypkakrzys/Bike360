﻿using Bike360.IntegrationTests.Tests;
using Bike360.Persistence.DatabaseContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace Bike360.IntegrationTests.TestFixtures;

public class IntegrationTestsWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server").Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //TODO
        //fix issue with random using wrong database by test classes

        var connectionString = _dbContainer.GetConnectionString();

        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<Bike360DatabaseContext>));
            services.AddDbContext<Bike360DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        });

        builder.ConfigureServices(services =>
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<Bike360DatabaseContext>();
            dbContext.Database.Migrate();

            SeedDatabase(dbContext);
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    private static void SeedDatabase(Bike360DatabaseContext dbContext)
    {
        using var transaction = dbContext.Database.BeginTransaction();

        try
        {
            dbContext.Customers.AddRange(DataFixture.SampleCustomers);
            dbContext.Bikes.AddRange(DataFixture.SampleBikes);
            dbContext.SaveChanges();

            dbContext.Reservations.AddRange(DataFixture.SampleReservations);
            dbContext.SaveChanges();

            dbContext.ReservationBikes.AddRange(DataFixture.SampleReservationBikes);
            dbContext.SaveChanges();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}