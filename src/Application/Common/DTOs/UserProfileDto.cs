using Application.Common.DTOs;
using Domain.Enums;

public class UserProfileDto
{
    public string UserId { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = "";

    public MembershipType? MembershipType { get; set; }
    public bool HasActiveMembership { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public List<UserBookingDto> Bookings { get; set; } = new();
}