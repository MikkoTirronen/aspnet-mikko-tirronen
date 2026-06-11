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
}