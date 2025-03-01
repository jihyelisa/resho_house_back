using System;
using System.ComponentModel.DataAnnotations;

public class EventDto
{
    public int UserId { get; set; }
    public int? CategoryId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime? Date { get; set; }
    public string? Participants { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserDto? User { get; set; }
    public CategoryDto? Category { get; set; }
    public IEnumerable<EventImage>? EventImageList { get; set; }
}