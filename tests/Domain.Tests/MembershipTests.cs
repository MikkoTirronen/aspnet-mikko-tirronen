using Domain.Entities;
using Domain.Enums;
using FluentAssertions;

namespace Domain.Tests;

public class MembershipTests
{
    [Fact]
    public void Constructor_Should_Create_Active_Membership()
    {
        var userId = Guid.NewGuid().ToString();

        var membership = new Membership(userId, MembershipType.Premium);

        membership.UserId.Should().Be(userId);
        membership.MembershipType.Should().Be(MembershipType.Premium);
        membership.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Constructor_Should_Set_EndDate_About_One_Month_After_StartDate()
    {
        var membership = new Membership(
            Guid.NewGuid().ToString(),
            MembershipType.Elite);

        membership.EndDate.Should().BeAfter(membership.StartDate);
        membership.EndDate.Should().BeCloseTo(
            membership.StartDate.AddMonths(1),
            TimeSpan.FromSeconds(2));
    }
}