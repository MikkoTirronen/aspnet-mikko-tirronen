namespace Presentation.WebApp.Models;

public class BookingViewModel
{
    public Guid BookingId { get; set; }

    public Guid ClassId { get; set; }

    public string ClassName { get; set; } = "";

    public string Category { get; set; } = "";

    public string Instructor { get; set; } = "";

    public DateTime StartTime { get; set; }

    public bool CanCancel { get; set; }
}