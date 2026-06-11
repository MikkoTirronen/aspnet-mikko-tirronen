using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IMembershipRepository
{
    Task<Membership?> GetByUserIdAsync(string userId, CancellationToken ct);
    Task<bool> ExistsByUserIdAsync(string userId, CancellationToken ct);
    Task AddAsync(Membership membership, CancellationToken ct);
}