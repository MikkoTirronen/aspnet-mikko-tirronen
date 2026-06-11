namespace Domain.Entities;

public class Membership
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public string MembershipType { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public bool IsActive { get; set; }

    private Membership() { }

    public Membership(string userId, string membershipType)
    {
        UserId = userId;
        MembershipType = membershipType;
        StartDate = DateTime.UtcNow;
        IsActive = true;
    }
}