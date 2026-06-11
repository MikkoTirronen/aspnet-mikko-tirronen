
using Microsoft.Extensions.DependencyInjection;
using Application.Abstractions.Commands;
using Application.Abstractions.Queries;
using Infrastructure.Dispatchers;
using Application.Features.GymClasses.GetClassDetails;
using Application.Features.Memberships.GetMembership;
using Application.Common.DTOs;
using Application.Features.Memberships.CreateMembership;
using Application.Features.Bookings.BookClass;
using Application.Features.GymClasses.GetAllClasses;
using Application.Features.Bookings.CancelBooking;
using Application.Abstractions.Services;
using Application.Features.Memberships.Services;

namespace Presentation.WebApp.DependencyInjections.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Dispatchers
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();

        //Membership
        services.AddScoped<IQueryHandler<GetMembershipQuery, MembershipDto?>, GetMembershipHandler>();
        services.AddScoped<ICommandHandler<CreateMembershipCommand, bool>, CreateMembershipHandler>();

        //Classes
        services.AddScoped<IQueryHandler<GetAllClassesQuery, List<GymClassCardDto>>,
            GetAllClassesHandler>();
        services.AddScoped<IQueryHandler<GetClassDetailsQuery, GymClassDetailsDto?>, GetClassDetailsHandler>();

        //Booking
        services.AddScoped<ICommandHandler<BookClassCommand, bool>, BookClassHandler>();
        services.AddScoped<ICommandHandler<CancelBookingCommand, bool>, CancelBookingHandler>();

        services.AddScoped<IMembershipPlanService, MembershipPlanService>();
        return services;
    }
}