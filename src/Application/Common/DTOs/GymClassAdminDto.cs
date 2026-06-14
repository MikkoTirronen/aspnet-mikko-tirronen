namespace Application.Features.GymClasses.GetGymClassById;

public sealed class GymClassAdminDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = "";

    public string Instructor { get; init; } = "";

    public DateTime StartTime { get; init; }

    public int Capacity { get; init; }

    public string Category { get; init; } = "";
}