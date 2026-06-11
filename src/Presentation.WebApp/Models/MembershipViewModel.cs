namespace Presentation.WebApp.Models;
public class MembershipPlanViewModel
{
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public string Description { get; set; } = "";
    public List<string> Features { get; set; } = [];
}