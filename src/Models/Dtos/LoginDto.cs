using System.ComponentModel.DataAnnotations;

public class LoginDto
{
    [Required]  // ✅ 이제 정상적으로 인식됨!
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}