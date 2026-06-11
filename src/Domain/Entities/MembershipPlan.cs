namespace Domain.Entities;

public class MembershipPlan
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public List<string> Features { get; set; } = [];
}