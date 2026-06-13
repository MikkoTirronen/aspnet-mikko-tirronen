
using Domain.Entities;
using FluentAssertions;

namespace Domain.Tests;

public class GymClassTests
{
    [Fact]
    public void BookClass_Should_Add_Booking()
    {
        var gymClass = new GymClass(
            "Yoga",
            "Anna",
            DateTime.UtcNow.AddDays(1),
            10,
            "Group Training");

        var userId = Guid.NewGuid().ToString();

        gymClass.Book(userId);

        gymClass.Bookings.Should().ContainSingle();
    }

    [Fact]
    public void BookClass_Should_Not_Allow_Duplicate_Booking()
    {
        var gymClass = new GymClass(
            "Yoga",
            "Anna",
            DateTime.UtcNow.AddDays(1),
            10,
            "Group Training");

        var userId = Guid.NewGuid().ToString();

        gymClass.Book(userId);

        Action act = () => gymClass.Book(userId);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void CancelBooking_Should_Remove_Booking()
    {
        var gymClass = new GymClass(
            "Yoga",
            "Anna",
            DateTime.UtcNow.AddDays(1),
            10,
            "Group Training");

        var userId = Guid.NewGuid().ToString();

        gymClass.Book(userId);
        gymClass.CancelBooking(userId);

        gymClass.Bookings.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_Should_Store_StartTime_As_Utc()
    {
        var startTime = new DateTime(2026, 6, 13, 12, 0, 0);

        var gymClass = new GymClass(
            "Yoga",
            "Anna",
            startTime,
            10,
            "Group Training");

        gymClass.StartTime.Kind.Should().Be(DateTimeKind.Utc);
    }
    [Fact]
    public void CancelBooking_Should_Do_Nothing_When_User_Has_No_Booking()
    {
        var gymClass = new GymClass(
            "Yoga",
            "Anna",
            DateTime.UtcNow.AddDays(1),
            10,
            "Group Training");

        Action act = () => gymClass.CancelBooking(Guid.NewGuid().ToString());

        act.Should().NotThrow();
        gymClass.Bookings.Should().BeEmpty();
    }
}