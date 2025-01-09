using Bike360.Application.Contracts.Email;
using Bike360.Infrastructure.Email;
using Microsoft.Extensions.DependencyInjection;

namespace Bike360.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
