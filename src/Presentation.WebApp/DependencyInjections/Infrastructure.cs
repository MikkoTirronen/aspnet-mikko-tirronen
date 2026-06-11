using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Presentation.WebApp.DependencyInjections;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
    IConfiguration config
    )
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IMembershipService, MembershipService>();
        services.AddScoped<IGymClassService, GymClassService>();
        
        return services;
    }
}