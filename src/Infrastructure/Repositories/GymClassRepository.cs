using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;

public class GymClassRepository : IGymClassRepository
{
    private readonly AppDbContext _context;

    public GymClassRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GymClass>> GetAllAsync(CancellationToken ct)
    {
        return await _context.GymClasses
            .AsNoTracking()
            .OrderBy(x => x.StartTime)
            .ToListAsync(ct);
    }

    public async Task<GymClass?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.GymClasses
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task AddAsync(GymClass gymClass, CancellationToken ct)
    {
        await _context.GymClasses.AddAsync(gymClass, ct);
    }

    public Task DeleteAsync(GymClass gymClass, CancellationToken ct)
    {
        _context.GymClasses.Remove(gymClass);
        return Task.CompletedTask;
    }
}