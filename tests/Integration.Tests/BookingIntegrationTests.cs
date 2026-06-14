using Application.Features.Bookings.BookClass;
using Application.Features.Bookings.CancelBooking;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Integration.Tests;

public class BookingIntegrationTests
{
    [Fact]
    public async Task BookClass_Should_Save_Booking_To_Database()
    {
        await using var db = TestDbContextFactory.Create();

        var gymClass = new GymClass(
            "Yoga",
            "Anna",
            DateTime.UtcNow.AddDays(1),
            10,
            "Group Training");

        db.GymClasses.Add(gymClass);
        await db.SaveChangesAsync();

        var classRepo = new GymClassRepository(db);
        var bookingRepo = new BookingRepository(db);

        var unitOfWork = db;

        var handler = new BookClassHandler(classRepo, bookingRepo, unitOfWork);

        var userId = Guid.NewGuid().ToString();

        var result = await handler.Handle(
            new BookClassCommand(gymClass.Id, userId),
            CancellationToken.None);

        result.Should().BeTrue();

        db.Bookings.Should().ContainSingle();
    }

    [Fact]
    public async Task BookClass_Should_Not_Save_Duplicate_Booking()
    {
        await using var db = TestDbContextFactory.Create();

        var gymClass = new GymClass(
            "Yoga",
            "Anna",
            DateTime.UtcNow.AddDays(1),
            10,
            "Group Training");

        db.GymClasses.Add(gymClass);
        await db.SaveChangesAsync();

        var classRepo = new GymClassRepository(db);
        var bookingRepo = new BookingRepository(db);
        var unitOfWork = db;

        var handler = new BookClassHandler(
            classRepo,
            bookingRepo,
            unitOfWork);

        var userId = Guid.NewGuid().ToString();

        var firstResult = await handler.Handle(
            new BookClassCommand(gymClass.Id, userId),
            CancellationToken.None);

        var secondResult = await handler.Handle(
            new BookClassCommand(gymClass.Id, userId),
            CancellationToken.None);

        firstResult.Should().BeTrue();
        secondResult.Should().BeFalse();

        db.Bookings.Should().ContainSingle();
    }

    [Fact]
    public async Task CancelBooking_Should_Remove_Booking_From_Database()
    {
        await using var db = TestDbContextFactory.Create();

        var gymClass = new GymClass(
            "Yoga",
            "Anna",
            DateTime.UtcNow.AddDays(1),
            10,
            "Group Training");

        db.GymClasses.Add(gymClass);
        await db.SaveChangesAsync();

        var classRepo = new GymClassRepository(db);
        var bookingRepo = new BookingRepository(db);
        var unitOfWork = db;

        var bookHandler = new BookClassHandler(
            classRepo,
            bookingRepo,
            unitOfWork);

        var cancelHandler = new CancelBookingHandler(
            bookingRepo,
            unitOfWork);

        var userId = Guid.NewGuid().ToString();

        var bookResult = await bookHandler.Handle(
            new BookClassCommand(gymClass.Id, userId),
            CancellationToken.None);

        bookResult.Should().BeTrue();

        var booking = await db.Bookings.SingleAsync();

        var cancelResult = await cancelHandler.Handle(
            new CancelBookingCommand(booking.Id, userId),
            CancellationToken.None);

        cancelResult.Success.Should().BeTrue();
        cancelResult.Value.Should().Be(gymClass.Id);

        db.ChangeTracker.Clear();

        var bookingStillExists = await db.Bookings.AnyAsync();

        bookingStillExists.Should().BeFalse();
    }
}