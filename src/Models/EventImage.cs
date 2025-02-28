using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EventImage
{
    [Key]
    public int id { get; set; }

    public int event_id { get; set; }

    [Required]
    public string image_url { get; set; } = string.Empty;

    // [ForeignKey("EventId")]
    // public virtual Event Event { get; set; } = new Event();
}