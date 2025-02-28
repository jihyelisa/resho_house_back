using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Notification
{
    [Key]
    public int id { get; set; }

    [Required]
    public int recieving_user_id { get; set; }

    [Required]
    public int acting_user_id { get; set; }

    [Required]
    [StringLength(50)]
    public string type { get; set; } = string.Empty;

    public bool read { get; set; } = false;

    public DateTime created_at { get; set; } = DateTime.UtcNow;

    // [ForeignKey("RecieveUserId")]
    // public virtual User RecieveUser { get; set; } = new User();

    // [ForeignKey("ActUserId")]
    // public virtual User ActUser { get; set; } = new User();
}