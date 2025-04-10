using System;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }

    public string? ProfileImageUrl { get; set; }

    [StringLength(50)]
    public string? Username { get; set; } = string.Empty;

    public string? Bio { get; set; }

    public DateTime? Birthday { get; set; }

    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? SignedInAt { get; set; }
}