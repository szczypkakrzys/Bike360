using Bike360.Application.Features.Reservations.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bike360.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<IReservationService, ReservationService>();

        return services;
    }
}
