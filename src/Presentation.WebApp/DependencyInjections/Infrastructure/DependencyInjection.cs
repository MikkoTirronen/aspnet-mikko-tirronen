
using Application.Abstractions.Repositories;
using Application.Abstractions.UnitOfWork;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Presentation.WebApp.DependencyInjections.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString, IWebHostEnvironment environment, IConfiguration config)
    {
        if (environment.IsDevelopment())
        {
            services.AddDbContext<AppDbContext>(options =>
                   options.UseInMemoryDatabase("CoreFitnessDevDb"));
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
                 options.UseNpgsql(connectionString));
        }


        services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        var githubClientId = config["Authentication:GitHub:ClientId"];
        var githubClientSecret = config["Authentication:GitHub:ClientSecret"];

        if (!string.IsNullOrWhiteSpace(githubClientId) &&
            !string.IsNullOrWhiteSpace(githubClientSecret))
        {
            services
                .AddAuthentication()
                .AddGitHub(options =>
                {
                    options.ClientId = githubClientId;
                    options.ClientSecret = githubClientSecret;
                    options.Scope.Add("user:email");
                });
        }

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<AppDbContext>());

        services.AddScoped<IMembershipRepository, MembershipRepository>();
        services.AddScoped<IGymClassRepository, GymClassRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();

        return services;
    }
}