using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class MembershipService(AppDbContext context) : IMembershipService
{
    private readonly AppDbContext _context = context;

    public async Task<Membership?> GetMembershipAsync(string userId, CancellationToken ct)
    {
        return await _context.Memberships.FirstOrDefaultAsync(x => x.UserId == userId,ct);
    }

    public async Task<bool> CreateMembershipAsync(string userId, string membershipType, CancellationToken ct)
    {
        var exists = await _context.Memberships.AnyAsync(x => x.UserId == userId);
        if (exists)
            return false;
        var membership = new Membership(userId, membershipType);

        _context.Memberships.Add(membership);

        await _context.SaveChangesAsync(ct);

        return true;
    }
}