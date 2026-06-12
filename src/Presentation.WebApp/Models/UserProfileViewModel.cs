namespace Presentation.WebApp.Models;

public class UserProfileViewModel
{
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";

    public MembershipViewModel? Membership { get; set; }

    public List<BookingViewModel> Bookings { get; set; } = [];
}