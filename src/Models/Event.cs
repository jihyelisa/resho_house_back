using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Event
{
    [Key]
    public int id { get; set; }

    [Required]
    public int user_id { get; set; }

    public int? category_id { get; set; }

    [Required]
    [StringLength(255)]
    public string title { get; set; } = string.Empty;

    public string? description { get; set; }

    public DateTime created_at { get; set; } = DateTime.UtcNow; // Ensure Utc kind
    public DateTime? updated_at { get; set; }

    // 관계 설정
    // [ForeignKey("UserId")]
    // public virtual User User { get; set; } = new User();

    // [ForeignKey("CategoryId")]
    // public virtual Category? Category { get; set; }
}