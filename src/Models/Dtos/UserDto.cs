using System.ComponentModel.DataAnnotations;

public class UserDto
{
    public int Id { get; set; }
    [Required]
    public string Email { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string? ProfileImageUrl { get; set; }
    public DateTime? Birthday { get; set; }
}