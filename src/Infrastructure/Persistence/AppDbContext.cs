
using Application.Abstractions.UnitOfWork;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUser>, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Membership> Memberships { get; set; } = null!;
    public DbSet<GymClass> GymClasses { get; set; } = null!;
    public DbSet<GymClassBooking> Bookings { get; set; } = null!;

    Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken ct)
        => base.SaveChangesAsync(ct);
}