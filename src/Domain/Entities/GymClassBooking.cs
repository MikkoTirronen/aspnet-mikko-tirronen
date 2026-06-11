namespace Domain.Entities;

public class GymClassBooking
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid GymClassId { get; private set; }
    public Guid UserId { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private GymClassBooking() { }

    public GymClassBooking(Guid gymClassId, Guid userId)
    {
        GymClassId = gymClassId;
        UserId = userId;
    }
}