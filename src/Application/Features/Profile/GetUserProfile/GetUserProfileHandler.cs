using Application.Abstractions.Queries;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common.DTOs;
using Application.Features.Profile.GetUserProfile;

namespace Application.Features.Profile;

public class GetUserProfileQueryHandler
    : IQueryHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly IUserService _userService;
    private readonly IBookingRepository _bookingRepo;
    private readonly IMembershipRepository _membershipRepo;

    public GetUserProfileQueryHandler(
        IUserService userService,
        IBookingRepository bookingRepo,
        IMembershipRepository membershipRepo)
    {
        _userService = userService;
        _bookingRepo = bookingRepo;
        _membershipRepo = membershipRepo;
    }

    public async Task<UserProfileDto> Handle(
        GetUserProfileQuery request,
        CancellationToken ct)
    {
        var user = await _userService.GetByIdAsync(request.UserId);

        if (user == null)
            throw new Exception("User not found");

        var membership = await _membershipRepo.GetByUserIdAsync(request.UserId, ct);

        var bookings = await _bookingRepo.GetUserBookings(request.UserId, ct);

        return new UserProfileDto
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email ?? "",
            PhoneNumber = user.PhoneNumber,

            MembershipType = membership?.MembershipType,
            HasActiveMembership = membership?.IsActive ?? false,

            StartDate = membership?.StartDate,
            EndDate = membership?.EndDate,

            Bookings = bookings.Select(b => new UserBookingDto
            {
                BookingId = b.Id,
                ClassName = b.GymClass.Name,
                StartTime = b.GymClass.StartTime
            }).ToList()
        };
    }
}