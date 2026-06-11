public record GymClassDetailsDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public string Instructor { get; init; } = "";
    public DateTime StartTime { get; init; }
    public int Capacity { get; init; }
    public string Category { get; init; }
    public int BookedCount { get; init; }
    public bool IsBookedByUser { get; init; }
}