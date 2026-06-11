namespace Presentation.WebApp.Models;

public class GymClassDetailsViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
    public string Instructor { get; set; } = "";
    public DateTime StartTime { get; set; }
    public int Capacity { get; set; }
    public string Category { get; set; } = "";

    public int BookedCount { get; set; }
    public bool IsFull => BookedCount >= Capacity;
    public bool IsBookedByUser { get; set; }
    public Guid? BookingId { get; set; }
}