using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IGymClassRepository
{
    Task<List<GymClass>> GetAllAsync(CancellationToken ct);
    Task<GymClass?> GetByIdAsync(Guid id, CancellationToken ct);
}