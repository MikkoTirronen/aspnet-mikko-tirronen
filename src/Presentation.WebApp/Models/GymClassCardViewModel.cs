namespace Presentation.WebApp.Models;

public class GymClassCardViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public string Instructor { get; set; } = "";

    public DateTime StartTime { get; set; }

    public int Capacity { get; set; }

    public string Category { get; set; } = "";
}