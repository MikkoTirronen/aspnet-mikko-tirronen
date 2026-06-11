using Application.Abstractions.Commands;

namespace Application.Features.Bookings.CancelBooking;

public sealed record CancelBookingCommand(
    Guid ClassId,
    Guid UserId
) : ICommand<bool>;