using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models;
public class RegisterPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Password is required.")]
    [RegularExpression(
    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
    ErrorMessage = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one number and one special character.")]
    public string Password { get; set; } = "";
}