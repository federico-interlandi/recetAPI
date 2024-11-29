using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    public required string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(8, MinimumLength = 6)]
    public required string Password { get; set; } = String.Empty;
}