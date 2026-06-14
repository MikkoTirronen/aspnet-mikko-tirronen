using Application.Abstractions.Commands;

namespace Application.Features.Bookings.BookClass;

public sealed record BookClassCommand(
    Guid ClassId,
    string UserId
) : ICommand<bool>;