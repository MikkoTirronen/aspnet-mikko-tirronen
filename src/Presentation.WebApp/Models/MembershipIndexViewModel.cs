using Domain.Entities;

namespace Presentation.WebApp.Models;

public class MembershipIndexViewModel
{
    public Membership? CurrentMembership { get; set; }

    public List<MembershipPlanViewModel> Plans { get; set; } = [];
}