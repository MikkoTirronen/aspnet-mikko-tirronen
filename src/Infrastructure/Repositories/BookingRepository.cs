

using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsAsync(Guid classId, string userId, CancellationToken ct)
    {
        return _context.Bookings
            .AnyAsync(x => x.GymClassId == classId && x.UserId == userId, ct);
    }

    public Task<int> CountByClassAsync(Guid classId, CancellationToken ct)
    {
        return _context.Bookings
            .CountAsync(x => x.GymClassId == classId, ct);
    }

    public Task<GymClassBooking?> GetAsync(Guid classId, string userId, CancellationToken ct)
    {
        return _context.Bookings
            .FirstOrDefaultAsync(x => x.GymClassId == classId && x.UserId == userId, ct);
    }

    public async Task AddAsync(GymClassBooking booking, CancellationToken ct)
    {
        await _context.Bookings.AddAsync(booking, ct);
    }

    public Task RemoveAsync(GymClassBooking booking, CancellationToken ct)
    {
        _context.Bookings.Remove(booking);
        return Task.CompletedTask;
    }

    public Task<List<GymClassBooking>> GetUserBookings(string userId, CancellationToken ct)
    {
        return _context.Bookings
            .Include(x => x.GymClass)
            .Where(x => x.UserId == userId)
            .ToListAsync(ct);
    }
}