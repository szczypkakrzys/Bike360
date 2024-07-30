using Bike360.Application.Contracts.Identity;
using Bike360.Application.Models.Identity;
using Bike360.Identity.DbContext;
using Bike360.Identity.Models;
using Bike360.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bike360.Identity;

public static class IdentityServicesRegistration
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddDbContext<CustomersManagementIdentityDbContext>(options =>
            //TODO
            //consider standalone database for authentication purposes
            options.UseSqlServer(configuration.GetConnectionString("CustomersDatabaseConnectionString")));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<CustomersManagementIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserService, UserService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            };
        });

        return services;
    }
}
