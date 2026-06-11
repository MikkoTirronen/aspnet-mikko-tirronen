namespace Domain.Entities;

public class GymClass
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string Instructor { get; private set; } = string.Empty;
    public DateTime StartTime { get; private set; }
    public int Capacity { get; private set; }
    public string Category { get; private set; }

    private GymClass() { }

    public GymClass(string name, string instructor, DateTime startTime, int capacity, string category)
    {
        Name = name;
        Instructor = instructor;
        StartTime = DateTime.SpecifyKind(startTime, DateTimeKind.Utc);
        Capacity = capacity;
        Category = category;
    }


}
