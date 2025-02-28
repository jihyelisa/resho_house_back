using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EventParticipant
{
    [Key]
    public int id { get; set; }

    public int event_id { get; set; }
    public int user_id { get; set; }
    
    public DateTime created_at { get; set; } = DateTime.UtcNow;

    // [ForeignKey("EventId")]
    // public virtual Event Event { get; set; } = new Event();

    // [ForeignKey("UserId")]
    // public virtual User User { get; set; } = new User();
}