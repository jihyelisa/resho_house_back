using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    public string? Username { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; } = string.Empty;

    public string ProfileImageUrl { get; set; } = "/images/default-profile.jpg";
}