using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IBookingRepository
{
    Task<bool> ExistsAsync(Guid classId, string userId, CancellationToken ct);
    Task<int> CountByClassAsync(Guid classId, CancellationToken ct);

    Task<GymClassBooking?> GetByIdAsync(Guid bookingId, CancellationToken ct);
    Task<GymClassBooking?> GetByClassAndUserAsync(Guid classId, string userId, CancellationToken ct);
    Task AddAsync(GymClassBooking booking, CancellationToken ct);
    Task RemoveAsync(GymClassBooking booking, CancellationToken ct);

    Task<List<GymClassBooking>> GetUserBookings(string userId, CancellationToken ct);
}