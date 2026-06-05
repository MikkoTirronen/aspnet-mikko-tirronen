using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models;
public class RegisterPasswordViewModel
{
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}