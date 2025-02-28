using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public int? CategoryId { get; set; }
    [Required]
    [StringLength(255)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = new User();
    public Category Category { get; set; } = new Category();
}