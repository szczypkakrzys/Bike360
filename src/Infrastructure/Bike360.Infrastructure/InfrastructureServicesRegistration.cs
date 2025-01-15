using Bike360.Application.Contracts.Email;
using Bike360.Application.Models;
using Bike360.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bike360.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        return services;
    }
}
