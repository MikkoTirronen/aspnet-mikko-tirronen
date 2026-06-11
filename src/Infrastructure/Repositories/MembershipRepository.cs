using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;

public class MembershipRepository : IMembershipRepository
{
    private readonly AppDbContext _context;

    public MembershipRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Membership?> GetByUserIdAsync(string userId, CancellationToken ct)
    {
        return _context.Memberships
            .FirstOrDefaultAsync(x => x.UserId == userId, ct);
    }

    public Task<bool> ExistsByUserIdAsync(string userId, CancellationToken ct)
    {
        return _context.Memberships
            .AnyAsync(x => x.UserId == userId, ct);
    }

    public async Task AddAsync(Membership membership, CancellationToken ct)
    {
        _context.Memberships.Add(membership);
        await _context.SaveChangesAsync(ct);
    }
}