using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{
    [Key]
    public int id { get; set; }

    public int event_id { get; set; }

    [Required]
    public int user_id { get; set; }

    public int? parent_comment_id { get; set; }

    [Required]
    public string content { get; set; } = string.Empty;

    public DateTime created_at { get; set; } = DateTime.UtcNow;
    public DateTime? updated_at { get; set; }

    // [ForeignKey("EventId")]
    // public virtual Event Event { get; set; } = new Event();

    // [ForeignKey("UserId")]
    // public virtual User User { get; set; } = new User();

    // [ForeignKey("ParentCommentId")]
    // public virtual Comment? ParentComment { get; set; }
}