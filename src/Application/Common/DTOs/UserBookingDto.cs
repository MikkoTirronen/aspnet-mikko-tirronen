namespace Application.Common.DTOs;

public class UserBookingDto
{
    public Guid BookingId { get; set; }
    public string ClassName { get; set; } = "";
    public DateTime StartTime { get; set; }
}