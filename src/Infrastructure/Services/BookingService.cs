using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookingService(AppDbContext context) : IBookingService
{
    private readonly AppDbContext _context = context;

    public async Task BookAsync(Guid classId, Guid userId, CancellationToken ct)
    {
        var gymClass = await _context.GymClasses
            .FirstOrDefaultAsync(x => x.Id == classId, ct);

        if (gymClass is null)
            throw new Exception("Class not found");

        // 1. check capacity
        var bookingCount = await _context.Bookings
            .CountAsync(x => x.GymClassId == classId, ct);

        if (bookingCount >= gymClass.Capacity)
            throw new Exception("Class is full");

        // 2. prevent duplicate booking (extra safety)
        var alreadyBooked = await _context.Bookings
            .AnyAsync(x => x.GymClassId == classId && x.UserId == userId, ct);

        if (alreadyBooked)
            throw new Exception("Already booked");

        // 3. create booking
        var booking = new GymClassBooking(classId, userId);

        _context.Bookings.Add(booking);

        await _context.SaveChangesAsync(ct);
    }

    public async Task CancelAsync(Guid classId, Guid userId, CancellationToken ct)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(x => x.GymClassId == classId && x.UserId == userId, ct);

        if (booking is null)
            return;

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync(ct);
    }
}