using System;
using System.ComponentModel.DataAnnotations;

public class Notification
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ReceivingUserId { get; set; }

    [Required]
    public int ActingUserId { get; set; }

    [Required]
    [StringLength(50)]
    public string Type { get; set; } = string.Empty;

    public bool Read { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}