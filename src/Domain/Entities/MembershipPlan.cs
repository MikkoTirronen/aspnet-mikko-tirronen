using Domain.Enums;

namespace Domain.Entities;

public class MembershipPlan
{
    public MembershipType MembershipType { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public List<string> Features { get; set; } = [];
}