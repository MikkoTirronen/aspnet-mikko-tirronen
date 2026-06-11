using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IMembershipService
{
    Task<Membership?> GetMembershipAsync(string userId, CancellationToken ct);
    Task<bool> CreateMembershipAsync(string userId, string membershipType, CancellationToken ct);
}