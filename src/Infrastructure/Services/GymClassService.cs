using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class GymClassService(AppDbContext context) : IGymClassService
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<GymClass>> GetAllAsync(CancellationToken ct)
    {
        return await _context.GymClasses.AsNoTracking()
            .OrderBy(x => x.StartTime)
            .ToListAsync(ct);
    }

    public async Task<GymClass?> GetByIdAsync(
        Guid id, CancellationToken ct
    )
    {
        return await _context.GymClasses.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<GymClassDetailsDto?> GetDetailsAsync(Guid id, string userIdString, CancellationToken ct)
    {

        var userId = Guid.Parse(userIdString);

        var isBooked = await _context.Bookings
            .AnyAsync(x => x.GymClassId == id && x.UserId == userId, ct);
        Console.WriteLine(isBooked);
        return await _context.GymClasses
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new GymClassDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                Instructor = x.Instructor,
                StartTime = x.StartTime,
                Capacity = x.Capacity,
                Category = x.Category,
                BookedCount = _context.Bookings.Count(b => b.GymClassId == x.Id),
                IsBookedByUser = isBooked
            })
            .FirstOrDefaultAsync(ct);
    }
}