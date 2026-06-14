namespace Presentation.WebApp.Models;

public class GymClassBookingViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public string Instructor { get; set; } = "";

    public DateTime StartTime { get; set; }

    public string Category { get; set; } = "";

    public int Capacity { get; set; }

    public int BookedCount { get; set; }

    public int RemainingSpots => Capacity - BookedCount;

    public bool IsFull => RemainingSpots <= 0;
}