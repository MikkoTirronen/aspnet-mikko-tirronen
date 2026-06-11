using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IGymClassService
{
    Task<IEnumerable<GymClass>> GetAllAsync(CancellationToken ct);
    Task<GymClass?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<GymClassDetailsDto?> GetDetailsAsync(Guid id, string userIdString, CancellationToken ct);
}