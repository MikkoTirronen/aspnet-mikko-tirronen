using Application.Common.DTOs;
using Domain.Enums;

public class UserProfileDto
{
    public string UserId { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";

    public MembershipType? MembershipType { get; set; }
    public bool HasActiveMembership { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public List<UserBookingDto> Bookings { get; set; } = new();
}