namespace Domain.Entities;

public class GymClass
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string Instructor { get; private set; } = string.Empty;
    public DateTime StartTime { get; private set; }
    public int Capacity { get; private set; }
    public string Category { get; private set; } = string.Empty;

    private readonly List<GymClassBooking> _bookings = new();
    public IReadOnlyCollection<GymClassBooking> Bookings => _bookings;

    private GymClass() { }

    public GymClass(string name, string instructor, DateTime startTime, int capacity, string category)
    {
        Name = name;
        Instructor = instructor;
        StartTime = DateTime.SpecifyKind(startTime, DateTimeKind.Utc);
        Capacity = capacity;
        Category = category;
    }

    public void Book(Guid userId)
    {
        if (_bookings.Count >= Capacity)
            throw new InvalidOperationException("Class is full");

        if (_bookings.Any(x => x.UserId == userId))
            throw new InvalidOperationException("Already booked");

        _bookings.Add(new GymClassBooking(Id, userId));
    }

    public void CancelBooking(Guid userId)
    {
        var booking = _bookings.FirstOrDefault(x => x.UserId == userId);

        if (booking is null)
            return;

        _bookings.Remove(booking);
    }
}
