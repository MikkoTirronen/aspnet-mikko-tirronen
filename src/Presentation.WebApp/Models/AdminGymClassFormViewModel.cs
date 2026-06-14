using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models;

public class AdminGymClassFormViewModel
{
    public Guid? Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Instructor { get; set; } = "";

    [Required]
    public DateTime StartTime { get; set; } = DateTime.Now.AddDays(1);

    [Range(1, 100)]
    public int Capacity { get; set; } = 10;

    [Required]
    public string Category { get; set; } = "";
}