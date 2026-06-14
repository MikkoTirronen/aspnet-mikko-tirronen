namespace Domain.Entities;

public class GymClassBooking
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid GymClassId { get; private set; }
    public GymClass GymClass { get; private set; } = null!;

    public string UserId { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private GymClassBooking() { }

    public GymClassBooking(Guid gymClassId, string userId)
    {
        GymClassId = gymClassId;
        UserId = userId;
    }
}