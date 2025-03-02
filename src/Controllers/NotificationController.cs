using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/notifications")]
[ApiController]
[Authorize] // 인증된 사용자만 접근 가능
public class NotificationController : ControllerBase
{
    private readonly NotificationService _notificationService;

    public NotificationController(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    // 1️⃣ Create a notification
    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] NotificationDto notificationDto)
    {
        var newNotification = await _notificationService.CreateNotificationAsync(
            notificationDto.ReceivingUserId,
            notificationDto.ActingUserId,
            notificationDto.Type
        );

        return CreatedAtAction(nameof(GetUserNotifications), new { id = newNotification.Id }, newNotification);
    }

    // 2️⃣ Get the list of the user
    [HttpGet]
    public async Task<IActionResult> GetUserNotifications()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var notifications = await _notificationService.GetUserNotificationsAsync(userId);
        return Ok(notifications);
    }

    // 3️⃣ Read one notification
    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkNotificationAsRead(int id)
    {
        var success = await _notificationService.MarkNotificationAsReadAsync(id);
        return success ? Ok(new { message = "Notification marked as read." }) : NotFound();
    }

    // 4️⃣ Read all notifications
    [HttpPut("read-all")]
    public async Task<IActionResult> MarkAllNotificationAsRead()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var success = await _notificationService.MarkAllNotificationsAsReadAsync(userId);
        return success ? Ok(new { message = "All notifications marked as read." }) : NotFound();
    }

    // 5️⃣ Delete one notification
    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> DeleteNotificationsAsync(int id)
    {
        var success = await _notificationService.DeleteNotificationsAsync(id);
        return success ? Ok(new { message = "All notifications marked as read." }) : NotFound();
    }

    // 6️⃣ Delete all notifications
    [HttpDelete("delete-all")]
    public async Task<IActionResult> DeleteAllNotificationsAsync()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var success = await _notificationService.DeleteAllNotificationsAsync(userId);
        return success ? Ok(new { message = "All notifications deleted" }) : NotFound();
    }
}