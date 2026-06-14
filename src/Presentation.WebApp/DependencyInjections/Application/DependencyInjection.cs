
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
using Application.Features.Profile.GetUserProfile;
using Application.Features.Profile;
using Infrastructure.Identity;
using Application.Common.Results;
using Application.Features.Profile.DeleteAccount;
using Application.Features.Profile.UpdateProfile;
using Application.Features.GymClasses.CreateGymClass;
using Application.Features.GymClasses.UpdateGymClass;
using Application.Features.GymClasses.DeleteGymClass;
using Application.Features.GymClasses.GetGymClassById;

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
        services.AddScoped<ICommandHandler<CreateGymClassCommand, Guid>, CreateGymClassHandler>();
        services.AddScoped<ICommandHandler<UpdateGymClassCommand, Result<Guid>>, UpdateGymClassHandler>();
        services.AddScoped<ICommandHandler<DeleteGymClassCommand, Result<Guid>>, DeleteGymClassHandler>();

        //Booking
        services.AddScoped<ICommandHandler<BookClassCommand, bool>, BookClassHandler>();
        services.AddScoped<ICommandHandler<CancelBookingCommand, Result<Guid>>, CancelBookingHandler>();
        services.AddScoped<ICommandHandler<DeleteAccountCommand, bool>, DeleteAccountHandler>();
        services.AddScoped<ICommandHandler<UpdateProfileCommand, bool>, UpdateProfileHandler>();
        services.AddScoped<IMembershipPlanService, MembershipPlanService>();

        services.AddScoped<IQueryHandler<GetUserProfileQuery, UserProfileDto>,
            GetUserProfileQueryHandler>();
        services.AddScoped<IQueryHandler<GetGymClassByIdQuery, GymClassAdminDto?>, GetGymClassByIdHandler>();
        
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}