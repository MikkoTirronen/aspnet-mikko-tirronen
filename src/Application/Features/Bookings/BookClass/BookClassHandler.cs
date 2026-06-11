using Application.Abstractions.Repositories;
using Application.Abstractions.UnitOfWork;
using Application.Abstractions.Commands;
using Domain.Entities;
namespace Application.Features.Bookings.BookClass;

public sealed class BookClassHandler
    : ICommandHandler<BookClassCommand, bool>
{
    private readonly IGymClassRepository _classRepo;
    private readonly IBookingRepository _bookingRepo;
    private readonly IUnitOfWork _unitOfWork;
    public BookClassHandler(IGymClassRepository classRepo, IBookingRepository bookingRepo, IUnitOfWork unitOfWork)
    {
        _classRepo = classRepo;
        _bookingRepo = bookingRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(BookClassCommand command, CancellationToken ct)
    {
        var exists = await _bookingRepo.ExistsAsync(command.ClassId, command.UserId, ct);

        if (exists)
            return false;

        var gymClass = await _classRepo.GetByIdAsync(command.ClassId, ct);

        if (gymClass is null)
            return false;

        var count = await _bookingRepo.CountByClassAsync(command.ClassId, ct);

        if (count >= gymClass.Capacity)
            return false;

        var booking = new GymClassBooking(command.ClassId, command.UserId);

        await _bookingRepo.AddAsync(booking, ct);

        await _unitOfWork.SaveChangesAsync(ct); // ONLY HERE

        return true;
    }
}
