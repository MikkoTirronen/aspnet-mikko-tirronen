using Application.Abstractions.Commands;
using Application.Common.Results;

namespace Application.Features.Bookings.CancelBooking;

public sealed record CancelBookingCommand(
    Guid ClassId,
    string UserId
) : ICommand<Result<Guid>>;