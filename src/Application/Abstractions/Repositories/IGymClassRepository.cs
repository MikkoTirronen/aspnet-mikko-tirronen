using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IGymClassRepository
{
    Task<List<GymClass>> GetAllAsync(CancellationToken ct);
    Task<GymClass?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(GymClass gymClass, CancellationToken ct);
    Task DeleteAsync(GymClass gymClass, CancellationToken ct);
}