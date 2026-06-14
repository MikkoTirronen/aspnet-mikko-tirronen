using Domain.Enums;

namespace Domain.Entities;

public class Membership
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
    public MembershipType MembershipType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; private set; }
    public bool IsActive => DateTime.UtcNow <= EndDate;

    private Membership() { }

    public Membership(string userId, MembershipType membershipType)
    {
        UserId = userId;
        MembershipType = membershipType;
        StartDate = DateTime.UtcNow;
        EndDate = DateTime.UtcNow.AddMonths(1);
    }
}