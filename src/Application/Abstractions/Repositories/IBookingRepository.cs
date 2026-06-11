using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IBookingRepository
{
    Task<bool> ExistsAsync(Guid classId, Guid userId, CancellationToken ct);
    Task<int> CountByClassAsync(Guid classId, CancellationToken ct);

    Task<GymClassBooking?> GetAsync(Guid classId, Guid userId, CancellationToken ct);

    Task AddAsync(GymClassBooking booking, CancellationToken ct);
    Task RemoveAsync(GymClassBooking booking, CancellationToken ct);
}