using System;
using System.ComponentModel.DataAnnotations;

public class EventDto
{
    public int user_id { get; set; }
    public int? category_id { get; set; }
    
    [Required]
    [StringLength(255)]
    public string title { get; set; } = string.Empty;
    public string? description { get; set; }

    public DateTime created_at { get; set; } = DateTime.UtcNow;
}