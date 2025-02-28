using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Like
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    public int? EventId { get; set; }
    public int? CommentId { get; set; }

    // [ForeignKey("UserId")]
    // public virtual User User { get; set; } = new User();

    // [ForeignKey("EventId")]
    // public virtual Event? Event { get; set; }

    // [ForeignKey("CommentId")]
    // public virtual Comment? Comment { get; set; }
}