using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int id { get; set; }

    public string? profile_image_url { get; set; }

    [Required]
    [StringLength(50)]
    public string username { get; set; } = string.Empty;

    public DateTime? birthday { get; set; }

    [StringLength(100)]
    public string? email { get; set; }

    [Required]
    public string password_hash { get; set; } = string.Empty;

    public DateTime created_at { get; set; } = DateTime.UtcNow; // Ensure Utc kind
    public DateTime? updated_at { get; set; }
}