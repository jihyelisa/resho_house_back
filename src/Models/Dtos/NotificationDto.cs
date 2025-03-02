public class NotificationDto
{
    public int Id { get; set; }
    public int ReceivingUserId { get; set; }
    public int ActingUserId { get; set; }
    public string Type { get; set; } = string.Empty;
    public bool Read { get; set; }
    public DateTime CreatedAt { get; set; }
}