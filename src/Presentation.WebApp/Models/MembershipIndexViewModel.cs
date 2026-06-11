using Application.Common.DTOs;
using Domain.Entities;

namespace Presentation.WebApp.Models;

public class MembershipIndexViewModel
{
    public MembershipViewModel? CurrentMembership { get; set; }

    public List<MembershipPlanViewModel> Plans { get; set; } = [];
}