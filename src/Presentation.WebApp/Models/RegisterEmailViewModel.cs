using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models;

public class RegisterEmailViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}