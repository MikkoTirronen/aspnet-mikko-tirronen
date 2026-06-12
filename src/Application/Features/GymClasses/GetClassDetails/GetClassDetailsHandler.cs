using Application.Abstractions.Queries;
using Application.Abstractions.Repositories;
using Application.Common.DTOs;

namespace Application.Features.GymClasses.GetClassDetails;

public sealed class GetClassDetailsHandler
    : IQueryHandler<GetClassDetailsQuery, GymClassDetailsDto?>
{
    private readonly IGymClassRepository _classRepo;
    private readonly IBookingRepository _bookingRepo;

    public GetClassDetailsHandler(
        IGymClassRepository classRepo,
        IBookingRepository bookingRepo)
    {
        _classRepo = classRepo;
        _bookingRepo = bookingRepo;
    }

    public async Task<GymClassDetailsDto?> Handle(
        GetClassDetailsQuery query,
        CancellationToken ct)
    {
        var gymClass = await _classRepo.GetByIdAsync(query.ClassId, ct);

        if (gymClass is null)
            return null;

        if (query.UserId is null)
            return null;

        var isBooked = await _bookingRepo.ExistsAsync(
            query.ClassId,
            query.UserId,
            ct);

        var bookedCount = await _bookingRepo.CountByClassAsync(
            query.ClassId,
            ct);

        var booking = await _bookingRepo.GetByClassAndUserAsync(query.ClassId, query.UserId, ct);

        return new GymClassDetailsDto
        {
            Id = gymClass.Id,
            Name = gymClass.Name,
            Instructor = gymClass.Instructor,
            StartTime = gymClass.StartTime,
            Capacity = gymClass.Capacity,
            Category = gymClass.Category,
            BookedCount = bookedCount,
            IsBookedByUser = isBooked,
            BookingId = booking?.Id
        };
    }
}
