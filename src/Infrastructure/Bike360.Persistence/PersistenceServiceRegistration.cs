using Bike360.Application.Contracts.Persistence;
using Bike360.Persistence.DatabaseContext;
using Bike360.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bike360.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<Bike360DatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Bike360Database"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IBikeRepository, BikeRepository>();

        return services;
    }
}
