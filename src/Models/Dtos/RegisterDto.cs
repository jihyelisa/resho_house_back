using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    public string? Username { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; } = string.Empty;
}