using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EventImage
{
    [Key]
    public int Id { get; set; }

    public int EventId { get; set; }
    public int ImageOrder { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string IsMain { get; set; } = string.Empty;
}