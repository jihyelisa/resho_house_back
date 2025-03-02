using Microsoft.EntityFrameworkCore;

public class NotificationService
{
    private readonly AppDbContext _context;

    public NotificationService(AppDbContext context)
    {
        _context = context;
    }

    // 1️⃣ Create a notification
    public async Task<NotificationDto> CreateNotificationAsync(int receivingUserId, int actingUserId, string type)
    {
        var notification = new Notification
        {
            ReceivingUserId = receivingUserId,
            ActingUserId = actingUserId,
            Type = type,
            CreatedAt = DateTime.UtcNow
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        return new NotificationDto
        {
            Id = notification.Id,
            ReceivingUserId = notification.ReceivingUserId,
            ActingUserId = notification.ActingUserId,
            Type = notification.Type,
            Read = notification.Read,
            CreatedAt = notification.CreatedAt
        };
    }

    // 2️⃣ Get the list of the user
    public async Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(int userId)
    {
        return await _context.Notifications
            .Where(n => n.ReceivingUserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new NotificationDto
            {
                Id = n.Id,
                ReceivingUserId = n.ReceivingUserId,
                ActingUserId = n.ActingUserId,
                Type = n.Type,
                Read = n.Read,
                CreatedAt = n.CreatedAt
            })
            .ToListAsync();
    }

    // 3️⃣ Read one notification
    public async Task<bool> MarkNotificationAsReadAsync(int notificationId)
    {
        var notification = await _context.Notifications.FindAsync(notificationId);
        if (notification == null) return false;

        notification.Read = true;
        await _context.SaveChangesAsync();
        return true;
    }

    // 4️⃣ Read all notification
    public async Task<bool> MarkAllNotificationsAsReadAsync(int userId)
    {
        var notifications = await _context.Notifications
            .Where(n => n.ReceivingUserId == userId && !n.Read)
            .ToListAsync();

        if (!notifications.Any()) return false;

        foreach (var notification in notifications)
        {
            notification.Read = true;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    // 5️⃣ Delete one notification
    public async Task<bool> DeleteNotificationsAsync(int notificationId)
    {
        var notification = await _context.Notifications.FindAsync(notificationId);
        if (notification == null) return false;

        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
        return true;
    }

    // 6️⃣ Delete all notifications
    public async Task<bool> DeleteAllNotificationsAsync(int userId)
    {
        var notifications = await _context.Notifications
            .Where(n => n.ReceivingUserId == userId)
            .ToListAsync();

        if (!notifications.Any()) return true;

        _context.Notifications.RemoveRange(notifications);

        await _context.SaveChangesAsync();
        return true;
    }
}