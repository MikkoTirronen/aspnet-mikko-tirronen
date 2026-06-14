using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models;

public class UserProfileViewModel
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\- ]+$",
    ErrorMessage = "First name may only contain letters, spaces and hyphens.")]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\- ]+$",
    ErrorMessage = "First name may only contain letters, spaces and hyphens.")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Phone]
    public string? PhoneNumber { get; set; }

    public MembershipViewModel? Membership { get; set; }

    public List<BookingViewModel> Bookings { get; set; } = [];
}