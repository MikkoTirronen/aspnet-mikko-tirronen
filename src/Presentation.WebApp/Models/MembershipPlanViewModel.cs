using Domain.Enums;

namespace Presentation.WebApp.Models;

public class MembershipPlanViewModel
{
    public MembershipType MembershipType { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public string Description { get; set; } = "";
    public List<string> Features { get; set; } = [];
}