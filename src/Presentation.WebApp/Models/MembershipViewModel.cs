using Domain.Enums;

namespace Presentation.WebApp.Models;

public class MembershipViewModel
{
    public MembershipType? MembershipType { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}