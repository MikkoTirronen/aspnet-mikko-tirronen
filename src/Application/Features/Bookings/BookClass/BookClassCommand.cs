using Application.Abstractions.Commands;

namespace Application.Features.Bookings.BookClass;

public sealed record BookClassCommand(
    Guid ClassId,
    Guid UserId
) : ICommand<bool>;