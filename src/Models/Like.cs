using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Like
{
    [Key]
    public int id { get; set; }

    [Required]
    public int user_id { get; set; }

    public int? event_id { get; set; }
    public int? comment_id { get; set; }

    // [ForeignKey("UserId")]
    // public virtual User User { get; set; } = new User();

    // [ForeignKey("EventId")]
    // public virtual Event? Event { get; set; }

    // [ForeignKey("CommentId")]
    // public virtual Comment? Comment { get; set; }
}