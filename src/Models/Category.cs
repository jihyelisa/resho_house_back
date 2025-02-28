using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int id { get; set; }

    [Required]
    [StringLength(50)]
    public string category_name { get; set; } = string.Empty;
}