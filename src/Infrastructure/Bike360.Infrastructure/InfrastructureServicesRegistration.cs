using Bike360.Application.Contracts.Logging;
using Bike360.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bike360.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}
