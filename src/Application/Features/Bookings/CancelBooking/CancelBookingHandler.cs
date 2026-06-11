using Application.Abstractions.Commands;
using Application.Abstractions.Repositories;
using Application.Abstractions.UnitOfWork;

namespace Application.Features.Bookings.CancelBooking;

public sealed class CancelBookingHandler
    : ICommandHandler<CancelBookingCommand, bool>
{
    private readonly IBookingRepository _bookingRepo;
    private readonly IUnitOfWork _uow;

    public CancelBookingHandler(
        IBookingRepository bookingRepo,
        IUnitOfWork unitOfWork)
    {
        _bookingRepo = bookingRepo;
        _uow = unitOfWork;
    }

    public async Task<bool> Handle(CancelBookingCommand command, CancellationToken ct)
    {
        var booking = await _bookingRepo.GetAsync(
            command.ClassId,
            command.UserId,
            ct);

        if (booking is null)
            return false;

        await _bookingRepo.RemoveAsync(booking, ct);

        await _uow.SaveChangesAsync(ct);

        return true;
    }
}