using Application.Abstractions.Commands;
using Application.Abstractions.Repositories;
using Application.Abstractions.UnitOfWork;
using Application.Common.Results;

namespace Application.Features.Bookings.CancelBooking;

public sealed class CancelBookingHandler
    : ICommandHandler<CancelBookingCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(CancelBookingCommand command, CancellationToken ct)
    {
        var booking = await _bookingRepo.GetAsync(
            command.ClassId,
            command.UserId,
            ct);

        if (booking is null)
            return Result<Guid>.Fail("Booking not found");

        await _bookingRepo.RemoveAsync(booking, ct);

        await _uow.SaveChangesAsync(ct);

        return Result<Guid>.Ok(command.ClassId);
    }
}